using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UILoginView : UIBaseView
{
    public Text UserNameText;
    public Text PasswordText;
    public Button ConfirmBtn;
    public Button CloseBtn;

    private UILoginCtrl _uiLoginCtrl;
    private LoginModel _loginModel;

    #region 生命周期

    protected override void OnCreate()
    {
        //_uiLoginCtrl = new UILoginCtrl();
        //_loginModel = DataMgr.Instance.LoginModel;

        //UserNameText.text = _loginModel.UserName;
        //PasswordText.text = _loginModel.Password;
        //ConfirmBtn.onClick.AddListener(OnLoginBtnClick);
        CloseBtn.onClick.AddListener(this.CloseSelf);
    }

    protected override void OnShow()
    {
        Logger.Info("UILoginView  OnShow  11111...");
    }

    protected override void OnHide()
    {
        Logger.Info("UILoginView  OnHide  22222...");
    }

    protected override void OnClose()
    {
        //_uiLoginCtrl.Dispose();
    }

    protected override void OnAddListener()
    {
        
    }

    protected override void OnRemoveListener()
    {
        
    }

    #endregion

    #region 业务逻辑

    private void OnLoginBtnClick()
    {
        _uiLoginCtrl.Login(UserNameText.text, PasswordText.text);
    }

    #endregion
}