using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginData : BaseData
{
    private string UserName { get; set; }
    private string Password { get; set; }

    public override void Init()
    {
    }

    public void SetLoginData(string username, string password)
    {
        UserName = username;
        Password = password;
    }
}