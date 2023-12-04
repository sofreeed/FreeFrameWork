using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor;
using Debug = UnityEngine.Debug;

public class Proto2CSUtil
{
    [MenuItem("Tools/Proto2CS")]
    public static void Proto2Cs()
    {
        //Proto2CS的根目录
        var protoRootDir = Path.Combine(Environment.CurrentDirectory, "Proto2CS/");

        var protoDir = Path.Combine(protoRootDir, "Proto/"); //Proto文件的目录
        var protoCsDir = Path.Combine(Environment.CurrentDirectory, "Assets/Scripts/ProtoGen/"); //输出CS的目录
        var protocDir = Path.Combine(protoRootDir,
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "protoc.exe" : "protoc"); //Protoc.exe


        var directoryInfo = new DirectoryInfo(protoDir);
        foreach (var fileInfo in directoryInfo.GetFiles("*.proto"))
        {
            var argument2 = $"--csharp_out=\"{protoCsDir}\" --proto_path=\"{protoDir}\" " + fileInfo.Name;
            //Run(protocDir, argument2, waitExit: true);
            string[] res = ProcessUtil.RunCmd(protocDir, argument2);
            if (!string.IsNullOrEmpty(res[1]))
            {
                Debug.LogError("出现错误：" + res[1]);
            }
        }

        UnityEngine.Debug.Log("执行完毕!");
        AssetDatabase.Refresh();
    }
}