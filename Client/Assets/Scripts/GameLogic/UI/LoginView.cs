using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginView : MonoBehaviour
{
    private LoginModule _loginModule;
    private LoginData _loginData;

    void Start()
    {
        _loginModule = Game.Instance.LoginModule;
        _loginData = DataCenter.Instance.LoginData;
    }

    void Login(string username, string password)
    {
        //发送消息
    }
}