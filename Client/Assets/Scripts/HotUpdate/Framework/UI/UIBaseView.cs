using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBaseView : MonoBehaviour
{
    public UINameEnum UIName { set; get; }

    public bool IsShow { set; get; }

    #region 生命周期

    public void Create()
    {
        OnCreate();
    }

    /// 面板被实例化，Awake后，Start前
    protected abstract void OnCreate();

    public void Show()
    {
        IsShow = true;
        OnShow();
        OnAddListener();
    }

    protected abstract void OnShow();

    public void Hide()
    {
        IsShow = false;
        OnHide();
        OnRemoveListener();
    }

    protected abstract void OnHide();

    public void Close()
    {
        OnClose();
    }

    protected abstract void OnClose();

    protected virtual void OnAddListener()
    {
    }

    protected virtual void OnRemoveListener()
    {
    }
    
    public virtual void OnClickMaskArea()
    {
        CloseSelf();
    }

    #endregion

    protected void CloseSelf()
    {
        UIMgr.Instance.CloseWindow(UIName);
    }

    protected IUnRegister AddListener<T>(Action<T> action)
    {
        return DataMgr.Instance.AddListener<T>(action);
    }

    protected void RemoveListener<T>(Action<T> action)
    {
        DataMgr.Instance.RemoveListener<T>(action);
    }

    protected void Broadcast<T>(T e)
    {
        DataMgr.Instance.Broadcast(e);
    }
    
    
}