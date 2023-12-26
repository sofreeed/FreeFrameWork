using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMeshLocalize : MonoBehaviour
{
    
    private TMP_Text Text;
    void Awake()
    {
        Text = GetComponent<TMP_Text>();
        //Text.text = "";
    }

    
}

