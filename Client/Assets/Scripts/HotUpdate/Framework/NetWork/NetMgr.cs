using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Networks;
using UnityEngine;

public class NetMgr : Singleton<NetMgr>
{
    private TcpNetwork _network;
    private Dictionary<int, Action<int, byte[]>> _handlers = new Dictionary<int, Action<int, byte[]>>();

    private const string ACCESS_KEY = "123465";
    private byte redundant = 0;
    private byte zipFlag = 0;

    private NetMgr()
    {
    }

    public override void Init()
    {
        var loginHandler = new LoginHandler(_handlers);
        var heroHandler = new HeroHandler(_handlers);
    }

    public void Connect(string ip, int port, Action<object, int, string> onConnect,
        Action<object, int, string> onClosed)
    {
        _network ??= new TcpNetwork
        {
            ReceivePkgHandle = OnReceiveMsg
        };

        _network.OnConnect = onConnect;
        _network.OnClosed = onClosed;
        _network.SetHostPort(ip, port);
        _network.Connect();
    }

    public void SendMsg(int msgId, byte[] bytes)
    {
        var sendData = PackData(msgId, bytes);
        _network?.SendMessage(sendData);
    }

    private void OnReceiveMsg(byte[] bytes)
    {
        var newBytes = RecvMessage(bytes, out var msgId);
        if (newBytes != null)
        {
            if (_handlers.TryGetValue(msgId, out var handler))
            {
                handler.Invoke(msgId, newBytes);
            }
        }
    }

    private void Update()
    {
        _network?.UpdateNetwork();
    }

    public void Close()
    {
        _network?.Close();
    }

    public override void Dispose()
    {
        _network?.Dispose();
        _network = null;
        _handlers.Clear();
    }

    #region 封包、解包

    /// 封包
    private byte[] PackData(int msgID, byte[] orgResponse)
    {
        //1.协议长度(协议内容长度+(2+3+4+5+6的字节长度)) 4字节
        //2.消息ID 2字节
        //3.报序号 4字节         --已经移除
        //4.冗余字段 1字节
        //5.压缩标志 1字节
        //6.crc32值(1+2+3+7) 8字节
        //7.请求内容
        byte[] response = EncryptData(orgResponse);
        //发送包序
        //int recvIndexBig = IPAddress.HostToNetworkOrder(sendSeqIndex);
        //内容长度
        int protoLen = response.Length;
        //计算协议长度
        int sendLen = protoLen + GetMessageConstLen();
        int sendLenBig = IPAddress.HostToNetworkOrder(sendLen);
        //2.消息长度
        short uMsgID = (short)msgID;
        short msgIDBig = IPAddress.HostToNetworkOrder(uMsgID);
        //计算crc32
        ByteBuffer bBufferCrc = new ByteBuffer();
        bBufferCrc.WriteInt(sendLenBig);
        bBufferCrc.WriteShort(msgIDBig);
        //bBufferCrc.WriteInt(recvIndexBig);
        bBufferCrc.WriteBytes(orgResponse);
        uint uCrc32 = Algorithm.Crc32.Sum(bBufferCrc.ToBytes());
        Int64 lCrc32 = uCrc32;
        bBufferCrc.Close();
        //封装协议
        ByteBuffer bBuffer = new ByteBuffer();
        bBuffer.WriteInt(sendLenBig);
        bBuffer.WriteShort(msgIDBig);
        //bBuffer.WriteInt(recvIndexBig);
        bBuffer.WriteByte(redundant);
        bBuffer.WriteByte(zipFlag);
        bBuffer.WriteLong(IPAddress.HostToNetworkOrder(lCrc32));
        bBuffer.WriteBytes(response);
        byte[] buffer = bBuffer.ToBytes();

        // Debug.LogError("+---------------lCrc32 = "+lCrc32);
        // Debug.LogError("+---------------sendLen = "+sendLen);
        // Debug.LogError("+---------------uMsgID = "+uMsgID);
        // Debug.LogError("+---------------sendSeqIndex = "+sendSeqIndex);
        // Debug.LogError("+---------------strBase64 = "+Convert.ToBase64String(orgResponse, 0, orgResponse.Length));
        // Debug.LogError("+---------------all strBase64 = "+Convert.ToBase64String(buffer, 0, buffer.Length));

        bBuffer.Close();
        return buffer;
    }

    /// 解包
    private byte[] RecvMessage(byte[] data, out int msgID)
    {
        int start = 0;
        //消息长度
        int sendLenBig = BitConverter.ToInt32(data, start);
        int sendLen = IPAddress.NetworkToHostOrder(sendLenBig);
        start += 4;
        //内容长度
        int protoLen = sendLen - GetMessageConstLen();
        //2.消息ID
        short msgIDBig = BitConverter.ToInt16(data, start);
        msgID = IPAddress.NetworkToHostOrder(msgIDBig);
        start += 2;
        //3.报序号     --已经移除
        //int recvIndexBig = BitConverter.ToInt32(data, start);
        //int recvIndex = IPAddress.NetworkToHostOrder(recvIndexBig);
        //start += 4;
        //4.冗余字段 1字节
        start += 1;
        //5.压缩标志
        start += 1;
        //6.crc32值
        long crc32 = IPAddress.NetworkToHostOrder(BitConverter.ToInt64(data, start));
        start += 8;
        //7.请求内容
        byte[] orgResponse = data.Skip(start).Take(protoLen).ToArray();
        byte[] response = DecryptData(orgResponse);
        //计算crc32  
        ByteBuffer bBuffer1 = new ByteBuffer();
        bBuffer1.WriteInt(sendLenBig);
        bBuffer1.WriteShort(msgIDBig);
        //bBuffer1.WriteInt(recvIndexBig);
        bBuffer1.WriteBytes(response);
        uint uCrc32 = Algorithm.Crc32.Sum(bBuffer1.ToBytes());
        long lCrc32 = (long)uCrc32;
        bBuffer1.Close();
        //校验crc32是否一致
        if (lCrc32 == crc32)
        {
            return response;
        }
        else
        {
            string errMsg = " CrcError! server crc = " + crc32 + " client crc = " + lCrc32;
            errMsg = errMsg + " sendLen = " + sendLen;
            errMsg = errMsg + " msgID = " + msgID;
            //errMsg = errMsg + " recvIndex = " + recvIndex;
            errMsg = errMsg + " strBase64 = " + Convert.ToBase64String(response, 0, response.Length);
            errMsg = errMsg + " all strBase64 = " + Convert.ToBase64String(data, 0, data.Length);
            Debug.LogError("解析消息错误，CRC不一致..." + errMsg);
            return null;
        }
    }

    /// <summary>
    /// 获取协议固定部分长度
    /// </summary>
    private int GetMessageConstLen()
    {
        //2 + 4 + 1 + 1 + 8
        //移除报序号，变成
        //2 + 1 + 1 + 8
        return 12;
    }

    #endregion

    public byte[] EncryptData(byte[] bytes)
    {
        return Rc4.Encrypt(Encoding.ASCII.GetBytes(ACCESS_KEY), bytes);
    }

    public byte[] DecryptData(byte[] bytes)
    {
        return Rc4.Decrypt(Encoding.ASCII.GetBytes(ACCESS_KEY), bytes);
    }
}