using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

public class ResourcePackageUpdater
{
    private string packageName;
    private Action<ResourcePackage> onComplete;

    private ResourcePackage package;
    private ResourceDownloaderOperation downloader;

    private const int DownloadingMaxNum = 10;
    private const int FailedTryAgain = 3;


    public ResourcePackageUpdater(string packageName)
    {
        this.packageName = packageName;
    }

    public ResourcePackageUpdater(ResourcePackage package)
    {
        this.package = package;
    }

    public void BeginDownLoad(Action<ResourcePackage> onComplete)
    {
        this.onComplete = onComplete;
        if (package == null)
        {
            YooAssetManager.Instance.CreateOrGetPackage(packageName, resourcePackage =>
            {
                if (YooAssetManager.Instance.PlayMode != EPlayMode.HostPlayMode)
                {
                    onComplete(resourcePackage);
                    return;
                }
                package = resourcePackage;
                var operation = package.UpdatePackageVersionAsync();
                operation.Completed += OnGetStaticVersion;
            });
        }
        else
        {
            if (YooAssetManager.Instance.PlayMode != EPlayMode.HostPlayMode)
            {
                onComplete(package);
                return;
            }

            var operation = package.UpdatePackageVersionAsync();
            operation.Completed += OnGetStaticVersion;
        }
    }

    private void OnGetStaticVersion(AsyncOperationBase baseOperation)
    {
        var versionOperation = baseOperation as UpdatePackageVersionOperation;
        if (versionOperation.Status == EOperationStatus.Succeed)
        {
            var packageVersion = versionOperation.PackageVersion;
            var manifestOperation = package.UpdatePackageManifestAsync(packageVersion);
            manifestOperation.Completed += OnUpdateManifest;
        }
        else
        {
            Debug.LogError($"Package更新，获得版本失败:{versionOperation.Error}");
        }
    }

    private void OnUpdateManifest(AsyncOperationBase baseOperation)
    {
        var manifestOperation = baseOperation as UpdatePackageManifestOperation;
        if (manifestOperation.Status == EOperationStatus.Succeed)
        {
            //保存当前清单的版本
            manifestOperation.SavePackageVersion();
            CreateDownloader();
        }
        else
        {
            Debug.LogError($"Package更新，更新Manifest失败:{manifestOperation.Error}");
        }
    }

    private void CreateDownloader()
    {
        downloader = package.CreateResourceDownloader(DownloadingMaxNum, FailedTryAgain);
        if (downloader.TotalDownloadCount == 0)
        {
            ClearUnusedCacheFiles();
        }
        else
        {
            //int totalDownloadCount = downloader.TotalDownloadCount;
            //long totalDownloadBytes = downloader.TotalDownloadBytes;
            //float sizeMB = totalDownloadBytes / 1048576f;
            //sizeMB = Mathf.Clamp(sizeMB, 0.1f, float.MaxValue);
            //string totalSizeMB = sizeMB.ToString("f1");

            //注意：开发者需要在下载前检测磁盘空间不足
            //TODO：弹出确认框：更新大小：totalSizeMB，更新个数：totalDownloadCount

            downloader.OnDownloadOverCallback = OnDownloadCompleted;
            downloader.OnDownloadErrorCallback = OnDownloadErrorCallback;
            downloader.OnDownloadProgressCallback = OnDownloadProgressCallback;
            //downloader.Completed += OnDownloadCompleted;
            downloader.BeginDownload();
        }
    }

    private void OnDownloadCompleted(bool isSucceed)
    {
        if (downloader.Status != EOperationStatus.Succeed)
        {
            Debug.LogError($"Package更新，更新资源失败:{downloader.Error}");
        }

        ClearUnusedCacheFiles();
    }

    private void OnDownloadProgressCallback(int totalDownloadCount, int currentDownloadCount,
        long totalDownloadSizeBytes, long currentDownloadSizeBytes)
    {
        //TODO：做进度条
    }

    private void OnDownloadErrorCallback(string fileName, string error)
    {
        //下载文件失败
        Debug.LogError($"Package更新，下载文件失败，文件名:{fileName}    失败原因：{error}");
    }

    private void ClearUnusedCacheFiles()
    {
        var operation = package.ClearUnusedCacheFilesAsync();
        operation.Completed += OnClearUnusedCacheFilesCompleted;
    }

    private void OnClearUnusedCacheFilesCompleted(AsyncOperationBase obj)
    {
        onComplete?.Invoke(package);
        Debug.Log($"Package更新，更新完毕！");
    }
}