using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

public class YooAssetManager : MonoBehaviour
{
    public static YooAssetManager Instance;

    public EPlayMode PlayMode = EPlayMode.EditorSimulateMode;
    public string AssetServerAddress = "http://127.0.0.1/CDN";

    private List<ResourcePackage> packages = new List<ResourcePackage>();

    private const string DefaultPackageName = "DefaultPackage";
    public ResourcePackage DefaultPackage { set; get; }

    private void Awake()
    {
        Instance = this;
        YooAssets.Initialize();
        //YooAssets.SetOperationSystemMaxTimeSlice(30);
    }

    private void Start()
    {
    }

    private void Update()
    {
        foreach (var package in packages)
        {
            package.UnloadUnusedAssets();
        }
    }

    public void Init(Action initCallback)
    {
        CreatePackage(DefaultPackageName, package =>
        {
            DefaultPackage = package;
            YooAssets.SetDefaultPackage(package);
            initCallback?.Invoke();
        });
    }

    public void DefaultPackageUpdate(Action<ResourcePackage> callback)
    {
        ResourcePackageUpdater updater = new ResourcePackageUpdater(DefaultPackage);
        updater.BeginDownLoad(callback);
    }

    public void CreateOrGetPackage(string packageName, Action<ResourcePackage> initCallback)
    {
        var package = GetPackage(packageName);
        if (package != null)
        {
            initCallback(package);
        }
        else
        {
            StartCoroutine(CreatePackageInternal(packageName, initCallback));
        }
    }

    public ResourcePackage GetPackage(string packageName)
    {
        foreach (var package in packages)
        {
            if (package.PackageName == packageName)
                return package;
        }

        return null;
    }

    public void CreatePackage(string packageName, Action<ResourcePackage> callback)
    {
        StartCoroutine(CreatePackageInternal(packageName, callback));
    }

    private IEnumerator CreatePackageInternal(string packageName, Action<ResourcePackage> callback)
    {
        var playMode = PlayMode;
        var package = YooAssets.CreatePackage(packageName);

        // 编辑器下的模拟模式
        InitializationOperation initializationOperation = null;
        if (playMode == EPlayMode.EditorSimulateMode)
        {
            var createParameters = new EditorSimulateModeParameters();
            var pipeline = EDefaultBuildPipeline.BuiltinBuildPipeline.ToString();
            createParameters.SimulateManifestFilePath =
                EditorSimulateModeHelper.SimulateBuild(pipeline, packageName);
            initializationOperation = package.InitializeAsync(createParameters);
        }

        // 单机运行模式
        if (playMode == EPlayMode.OfflinePlayMode)
        {
            var createParameters = new OfflinePlayModeParameters();
            initializationOperation = package.InitializeAsync(createParameters);
        }

        // 联机运行模式
        if (playMode == EPlayMode.HostPlayMode)
        {
            var createParameters = new HostPlayModeParameters();
            //createParameters.QueryServices = null;
            //createParameters.DecryptionServices = null;
            //createParameters.RemoteServices = new RemoteServices(GetAssetServerURL(), GetAssetServerURL());
            initializationOperation = package.InitializeAsync(createParameters);
        }

        yield return initializationOperation;
        if (package.InitializeStatus == EOperationStatus.Succeed)
        {
            packages.Add(package);
            callback?.Invoke(package);
        }
        else
        {
            Debug.LogError($"创建ResourcePackage失败，包名:{packageName},Error{initializationOperation.Error}");
        }
    }

    /// <summary>
    /// 获取资源服务器地址
    /// </summary>
    private string GetAssetServerURL(string packageVersion)
    {
        //TODO:packageVersion 有待验证
        string gameVersion = packageVersion;
        //string gameVersion = "v1.0";

#if UNITY_EDITOR
        if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
            return $"{AssetServerAddress}/Android/{gameVersion}";
        else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS)
            return $"{AssetServerAddress}/IPhone/{gameVersion}";
        else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.WebGL)
            return $"{AssetServerAddress}/WebGL/{gameVersion}";
        else
            return $"{AssetServerAddress}/PC/{gameVersion}";
#else
		if (Application.platform == RuntimePlatform.Android)
			return $"{AssetServerAddress}/Android/{gameVersion}";
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
			return $"{AssetServerAddress}/IPhone/{gameVersion}";
		else if (Application.platform == RuntimePlatform.WebGLPlayer)
			return $"{AssetServerAddress}/WebGL/{gameVersion}";
		else
			return $"{AssetServerAddress}/PC/{gameVersion}";
#endif
    }
}