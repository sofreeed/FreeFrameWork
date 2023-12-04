using System.Collections;
using System.Collections.Generic;
using Google.Protobuf;
using ProtoCS;
using UnityEngine;

public class ProtoTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MsgTemplate msgTemplate = new MsgTemplate();
        msgTemplate.F = 3.14f;
        msgTemplate.Str = "Hello World";
        byte[] bytes = msgTemplate.ToByteArray();

        MsgTemplate msgTemplate1 = MsgTemplate.Parser.ParseFrom(bytes);
        float f = msgTemplate1.F;
        string str = msgTemplate1.Str;
        Debug.LogError(str);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
