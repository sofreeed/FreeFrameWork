using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBagView : UIBaseView
{
    public Button CloseBtn;
    
    protected override void OnCreate()
    {
        CloseBtn.onClick.AddListener(this.CloseSelf);
    }

    protected override void OnShow()
    {
        Logger.Info("UIBagView  OnShow  11111...");
    }

    protected override void OnHide()
    {
        Logger.Info("UIBagView  OnHide  22222...");
    }

    protected override void OnClose()
    {
        
    }
    
    
}
