using System.Collections;
using System.Collections.Generic;
using System.IO;
using Luban;
using UnityEngine;
using SimpleJSON;
using YooAsset;

public class DataTableMgr : Singleton<DataTableMgr>
{
    public static cfg.Tables Tables;

    private ResourcePackage _dataPackage;

    private DataTableMgr()
    {
    }

    public override void Init()
    {
        _dataPackage = YooAssetManager.Instance.GetPackage(GameConst.HotUpdatePackageName);
        if (_dataPackage == null)
        {
            Logger.Error("存放数据表的ResourcePackage为空 !!!");
        }

        var tables = new cfg.Tables(LoadJson);
        Tables = tables;
        
        //设置多语言表
        LocalizeMgr.Instance.ReLoad();
        //string localizeStr = tables.TbUIConfig[1000].Desc.Localize();
    }

    private ByteBuf LoadByte(string file)
    {
#if UNITY_EDITOR
        var path = Path.Combine(Application.dataPath, GameConst.LUBAN_DATA_PATH);
        return new ByteBuf(File.ReadAllBytes($"{path}{file}.bytes"));
#else
        var handle = _dataPackage.LoadRawFileSync(file);
        return new ByteBuf(handle.GetRawFileData());
#endif
        
    }

    private JSONNode LoadJson(string file)
    {
#if UNITY_EDITOR
        var path = Path.Combine(Application.dataPath, GameConst.LUBAN_DATA_PATH);
        var json = File.ReadAllText($"{path}{file}.json", System.Text.Encoding.UTF8);
        return JSON.Parse(json);
#else
        var handle = _dataPackage.LoadRawFileSync(file);
        return JSON.Parse(handle.GetRawFileText());
#endif
    }

    public override void Dispose()
    {
        _dataPackage = null;
        Tables = null;
    }
}