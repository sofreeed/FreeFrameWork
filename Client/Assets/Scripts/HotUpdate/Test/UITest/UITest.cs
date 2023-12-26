using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var dataTableMgr = DataTableMgr.Instance;
        var dataMgr = DataMgr.Instance;
        var netMgr = NetMgr.Instance;
        var uiMgr = UIMgr.Instance;
        
        
        uiMgr.OpenWindow(UINameEnum.Main);
    }

    // Update is called once per frame
    void OnGUI()
    {
        float x = 0f;
        float y = 0f;
        float w = 100;
        float h = 50;
        Rect r1 = new Rect(x, y, w, h);
        if (GUI.Button(r1,"login"))
        {
            UIMgr.Instance.OpenWindow(UINameEnum.Login);
        }

        x += 100;
        Rect r2 = new Rect(x, y, w, h);
        if (GUI.Button(r2,"hero"))
        {
            UIMgr.Instance.OpenWindow(UINameEnum.Hero);
        }
        
        x += 100;
        Rect r3 = new Rect(x, y, w, h);
        if (GUI.Button(r3,"bag"))
        {
            UIMgr.Instance.OpenWindow(UINameEnum.Bag);
        }
        
        x += 100;
        Rect r4 = new Rect(x, y, w, h);
        if (GUI.Button(r4,"shop"))
        {
            UIMgr.Instance.OpenWindow(UINameEnum.Shop);
        }
        
        x += 100;
        Rect r5 = new Rect(x, y, w, h);
        if (GUI.Button(r5,"buy"))
        {
            UIMgr.Instance.OpenWindow(UINameEnum.Buy);
        }
    }
}