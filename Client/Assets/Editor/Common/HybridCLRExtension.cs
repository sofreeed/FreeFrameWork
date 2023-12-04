using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using HybridCLR.Editor;
using UnityEditor;
using UnityEngine;

public class HybridCLRExtension
{
    [MenuItem("HybridCLR/CopyHotUpdateDll2Assets", priority = 1000)]
    public static void CopyHotUpdateDll2Assets()
    {
        var targetFolderPath = Path.Combine(Environment.CurrentDirectory,
            $"Assets/AssetsPackage/HotUpdateDlls/");

        DirectoryInfo directoryInfo = Directory.CreateDirectory(targetFolderPath);
        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        if (directoryInfo.Exists)
        {
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                file.Delete();
            }
        }

        List<string> hotDlls = SettingsUtil.HotUpdateAssemblyNamesIncludePreserved;
        foreach (var hotDll in hotDlls)
        {
            var source = Path.Combine(Environment.CurrentDirectory,
                SettingsUtil.GetHotUpdateDllsOutputDirByTarget(EditorUserBuildSettings.activeBuildTarget),
                $"{hotDll}.dll");

            var target = Path.Combine(targetFolderPath, $"{hotDll}.bytes");

            File.Copy(source, target);

            //Debug.LogError(target);
        }
        
        AssetDatabase.Refresh();
    }
}