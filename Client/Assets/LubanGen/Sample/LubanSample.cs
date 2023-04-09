using System.Collections;
using System.Collections.Generic;
using System.IO;
using Bright.Serialization;
using UnityEngine;
using SimpleJSON;

public class LubanSample : MonoBehaviour
{
    void Start()
    {
        var tablesCtor = typeof(cfg.Tables).GetConstructors()[0];
        var loaderReturnType = tablesCtor.GetParameters()[0].ParameterType.GetGenericArguments()[1];
        // 根据cfg.Tables的构造函数的Loader的返回值类型决定使用json还是ByteBuf Loader
        System.Delegate loader = loaderReturnType == typeof(ByteBuf) ?
            new System.Func<string, ByteBuf>(LoadByteBuf)
            : (System.Delegate)new System.Func<string, JSONNode>(LoadJson);
        var tables = (cfg.Tables)tablesCtor.Invoke(new object[] {loader});

        Debug.LogError("TbBase_1:" + tables.TbBase_1[3].MyString);

        //tables.TranslateText((key, text) => text + "# translate");
    }

    private static JSONNode LoadJson(string file)
    {
        return JSON.Parse(File.ReadAllText($"{Application.dataPath}/LubanGen/json/{file}.json", System.Text.Encoding.UTF8));
    }

    private static ByteBuf LoadByteBuf(string file)
    {
        return new ByteBuf(File.ReadAllBytes($"{Application.dataPath}/LubanGen/bytes/{file}.bytes"));
    }
    
}
