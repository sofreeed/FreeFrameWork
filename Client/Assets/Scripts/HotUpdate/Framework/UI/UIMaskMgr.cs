using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMaskMgr
{
    private Transform uiMaskGo;

    public void Init(Transform UIRoot)
    {
        if (uiMaskGo == null)
        {
            uiMaskGo = UIRoot.Find("UIMask");
        }
    }

    public void Show(UIBaseView view, float alpha, bool clickCross, bool clickClose)
    {
        if (uiMaskGo != null)
        {
            GameObject go = Object.Instantiate(uiMaskGo.gameObject, view.transform);
            go.SetActive(true);
            go.GetComponent<RectTransform>().SetAsFirstSibling();

            Image bg = go.GetComponent<Image>();
            var bgColor = bg.color;
            bgColor.a = alpha;
            bg.color = bgColor;
            
            Button btn = go.GetComponent<Button>();
            btn.interactable = !clickCross;
            
            if(clickClose)
                btn.onClick.AddListener(view.OnClickMaskArea);
        }
    }

    public void Dispose()
    {
        Object.Destroy(uiMaskGo);
        uiMaskGo = null;
    }
}