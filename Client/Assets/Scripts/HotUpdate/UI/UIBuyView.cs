using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyView : UIBaseView
{
    public Button CloseBtn;
    protected override void OnCreate()
    {
        CloseBtn.onClick.AddListener(this.CloseSelf);
    }

    protected override void OnShow()
    {
        
    }

    protected override void OnHide()
    {
        
    }

    protected override void OnClose()
    {
        
    }
}
