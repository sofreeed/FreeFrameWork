using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private void Start()
    {
        var dataTableMgr = DataTableMgr.Instance;
        var dataMgr = DataMgr.Instance;
        var netMgr = NetMgr.Instance;
        var uiMgr = UIMgr.Instance;
    }
}