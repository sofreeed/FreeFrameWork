using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopView : UIBaseView
{
    public Button CloseBtn;

    protected override void OnCreate()
    {
        CloseBtn.onClick.AddListener(this.CloseSelf);
    }

    protected override void OnShow()
    {
        Logger.Info("UIShopView  OnShow  11111...");
    }

    protected override void OnHide()
    {
        Logger.Info("UIShopView  OnHide  22222...");
    }

    protected override void OnClose()
    {
    }
}