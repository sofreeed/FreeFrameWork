using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizeTextEditor_CN
{
    
}

public class TextMeshLocalize : MonoBehaviour
{
    public string Key;
    private TMP_Text Text;

    void Awake()
    {
        Text = GetComponent<TMP_Text>();
        
        if (!string.IsNullOrEmpty(Key))
            Text.text = Key.Localize();
    }
}