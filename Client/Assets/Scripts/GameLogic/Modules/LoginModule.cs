using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct S2CLoginEvent
{
    public string username;
    public string password;
}

public class LoginModule : BaseModule
{
    private LoginData _loginData;
    
    public override void Init()
    {
        _loginData = DataCenter.Instance.LoginData;
        
        TypeEventSystem.Global.Register<S2CLoginEvent>(OnLogin);
    }

    public override void Dispose()
    {
        TypeEventSystem.Global.UnRegister<S2CLoginEvent>(OnLogin);
    }


    public void OnLogin(S2CLoginEvent e)
    {
        _loginData.SetLoginData(e.username, e.password);
    }
}
