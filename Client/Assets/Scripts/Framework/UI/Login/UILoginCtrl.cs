using System.Collections;
using System.Collections.Generic;
using Google.Protobuf;
using ProtoCS;
using UnityEngine;

public class UILoginCtrl : UIBaseCtrl
{
    private LoginModel _loginModel;

    public UILoginCtrl()
    {
        _loginModel = DataMgr.Instance.LoginModel;
    }

    public void Login(string userName, string password)
    {
        LoginC2S loginC2S = new LoginC2S();
        loginC2S.Username = userName;
        loginC2S.Password = password;
        byte[] bytes = loginC2S.ToByteArray();
        
        NetMgr.Instance.SendMsg(MsgIdDefine.LoginC2S, bytes);
    }

    public override void Dispose()
    {
    }
}