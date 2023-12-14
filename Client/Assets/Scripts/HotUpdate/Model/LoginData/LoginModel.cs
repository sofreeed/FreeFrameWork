using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginModel : BaseModel
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public LoginModel()
    {
        UserName = PlayerPrefs.GetString("username");
        Password = PlayerPrefs.GetString("password");
    }

    public void Login(string username, string password)
    {
        SetAccountInfo(username, password);
        //LoginEvent eventData = new LoginEvent(username, password);
        //this.Broadcast(eventData);
        //TODO:登录成功，切换场景
    }

    public void Logout()
    {
    }

    public void SetAccountInfo(string username, string password)
    {
        this.UserName = username;
        this.Password = password;
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("password", password);
    }

    public override void Dispose()
    {
        base.Dispose(); //清理事件注册
    }
}