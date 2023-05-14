using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : BaseData
{
    public string Name { get; set; }
    public int Level{ get; set; }
    
    public override void Init()
    {
        
    }

    public void SetPlayerData(string name, int level)
    {
        Name = name;
        Level = level;
    }
}
