using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HybridCLR;
using UnityEngine;
using YooAsset;

public class Launcher : MonoBehaviour
{
    private const string HotUpdatePackageName = "HotUpdatePackage";
    private const string HotUpdateDllName = "HotUpdate";
    private const string HotUpdatePrefabName = "HotUpdateMain";

    void Start()
    {
        YooAssetManager.Instance.Init(() => { YooAssetManager.Instance.DefaultPackageUpdate(OnAssetUpdateComplete); });
    }

    public void OnAssetUpdateComplete(ResourcePackage package)
    {
        ResourcePackageUpdater hotUpdateDll = new ResourcePackageUpdater(HotUpdatePackageName);
        hotUpdateDll.BeginDownLoad(OnHotUpdateDllComplete);
    }

    public void OnHotUpdateDllComplete(ResourcePackage package)
    {
#if UNITY_EDITOR
        // Editor下无需加载，直接查找获得HotUpdate程序集
        //Assembly hotUpdateAss = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "HotUpdate");
#else
        LoadMetadataForAOTAssemblies(package);

        var handle = package.LoadRawFileSync(HotUpdateDllName);
        Assembly hotUpdateAss = Assembly.Load(handle.GetRawFileData());
        Debug.Log($"热更新dll加载成功，大小{handle.GetRawFileData().Length}");
#endif

        var handle1 = YooAssets.LoadAssetSync<GameObject>(HotUpdatePrefabName);
        GameObject go = handle1.InstantiateSync();
        //进入HotUpdateMain.Start()
    }

    private static void LoadMetadataForAOTAssemblies(ResourcePackage package)
    {
        List<string> aotDllList = new List<string>
        {
            //"mscorlib.dll",
            //"System.dll",
            //"System.Core.dll", // 如果使用了Linq，需要这个
        };

        foreach (var aotDllName in aotDllList)
        {
            var handle = package.LoadRawFileSync(aotDllName);
            var res =
                RuntimeApi.LoadMetadataForAOTAssembly(handle.GetRawFileData(), HomologousImageMode.SuperSet);
            Debug.Log($"LoadMetadataForAOTAssembly:{aotDllName}. ret:{res}");
        }
    }
}