using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{

    public static TypeEventSystem EventSystem;
    public static cfg.Tables Tables;

    void Start()
    {
        EventSystem = TypeEventSystem.Global;
        Tables = DataTableManager.Init();
        
        Game.Instance.Init();
        DataCenter.Instance.Init();
    }

    
    void Update()
    {
        
    }

    private void InitTables()
    {
        
    }
}
