using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YooAsset;

public class HotUpdateMain : MonoBehaviour
{
    void Start()
    {
        Logger.Info("HotUpdateMain Game Start ...");

        var dataTableMgr = DataTableMgr.Instance;
        var dataMgr = DataMgr.Instance;
        var netMgr = NetMgr.Instance;
        var uiMgr = UIMgr.Instance;
        
        //gameObject.AddComponent<UITest>();
        
        //var _rootAssetHandle = YooAssets.LoadAssetSync<GameObject>("UIRoot");
        //GameObject UIRoot = _rootAssetHandle.InstantiateSync();
    }
}