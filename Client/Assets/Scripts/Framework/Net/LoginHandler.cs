using System;
using System.Collections.Generic;
using ProtoCS;

public class LoginHandler
{
    public LoginHandler(IDictionary<int, Action<int, byte[]>> handlers)
    {
        handlers.Add(MsgIdDefine.LoginS2C, OnLogin);
        handlers.Add(MsgIdDefine.LogoutS2C, OnLogout);
    }

    private void OnLogin(int msgId, byte[] data)
    {
        //TODO：反序列化pb
        LoginS2C loginS2C = LoginS2C.Parser.ParseFrom(data);
        DataMgr.Instance.LoginModel.SetAccountInfo(loginS2C.Username, loginS2C.Password);
    }

    private void OnLogout(int msgId, byte[] data)
    {
    }
}