using System.Collections;
using System.Collections.Generic;
using System.IO;
using Luban;
using SimpleJSON;
using UnityEngine;

public class DataTest : MonoBehaviour
{
    public string DataPath { get; set; }

    void Start()
    {
        var tables = new cfg.Tables(LoadJson);
        Debug.LogError(tables.TbItem[10000].Name);
    }

    private static ByteBuf LoadByte(string file)
    {
        return new ByteBuf(File.ReadAllBytes($"{Application.dataPath}/../../GenerateDatas/bytes/{file}.bytes"));
    }

    private static JSONNode LoadJson(string file)
    {
        return JSON.Parse(File.ReadAllText(Application.dataPath + "/Scripts/LubanGen/Datas/" + file + ".json",
            System.Text.Encoding.UTF8));
    }
}