using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCenter : Singleton<DataCenter>
{
    public LoginData LoginData;
    public PlayerData PlayerData;
    
    private DataCenter()
    {
        
    }
    
    public void Init()
    {
        LoginData.Init();
        PlayerData.Init();
    }

}
