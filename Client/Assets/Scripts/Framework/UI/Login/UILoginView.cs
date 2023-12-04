using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UILoginView : UIBaseView
{
    public Text userNameText;
    public Text passwordText;
    public Button confirmBtn;

    private UILoginCtrl _uiLoginCtrl;
    private LoginModel _loginModel;

    #region 生命周期

    protected override void OnCreate()
    {
        _uiLoginCtrl = new UILoginCtrl();
        _loginModel = DataMgr.Instance.LoginModel;

        userNameText.text = _loginModel.UserName;
        passwordText.text = _loginModel.Password;
        confirmBtn.onClick.AddListener(OnLoginBtnClick);
    }

    protected override void OnShow()
    {
    }

    protected override void OnHide()
    {
    }

    protected override void OnClose()
    {
        _uiLoginCtrl.Dispose();
    }

    protected override void OnAddListener()
    {
        //this.AddListener<WindowOpen>(OnWindowOpen).UnRegisterWhenGameObjectDestroyed(this);
    }

    protected override void OnRemoveListener()
    {
        //使用自动反注册
        //this.RemoveListener<WindowOpen>(OnWindowOpen);
    }

    #endregion

    #region 业务逻辑

    private void OnLoginBtnClick()
    {
        _uiLoginCtrl.Login(userNameText.text, passwordText.text);
    }

    #endregion
}