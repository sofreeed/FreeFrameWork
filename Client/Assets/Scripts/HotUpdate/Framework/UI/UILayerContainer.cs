using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILayerContainer
{
    public UILayerEnum UILayer { set; get; }
    public Transform Transform { set; get; }

    public UILayerContainer(UILayerEnum uiLayer, Transform uiRoot)
    {
        UILayer = uiLayer;
        var go = new GameObject(uiLayer.ToString());
        var canvas = go.AddComponent<Canvas>();
        //go.AddComponent<CanvasScaler>();
        go.AddComponent<GraphicRaycaster>();

        Transform = go.transform;
        Transform.SetParent(uiRoot);
        Transform.SetAsLastSibling();
        
        canvas.overrideSorting = true;
        canvas.sortingOrder = 1000 * (int)uiLayer;

        var rectTran = go.GetComponent<RectTransform>();
        rectTran.anchorMin = Vector2.zero;
        rectTran.anchorMax = Vector2.one;
        rectTran.offsetMax = Vector2.one;
        rectTran.offsetMin = Vector2.one;

        Transform.localScale = Vector3.one;
    }
}