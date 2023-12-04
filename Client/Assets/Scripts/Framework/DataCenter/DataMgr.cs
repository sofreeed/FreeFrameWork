using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : Singleton<DataMgr>
{
    public LoginModel LoginModel { get; set; }
    public HeroModel HeroModel { get; set; }

    public override void Init()
    {
        LoginModel = new LoginModel();
        HeroModel = new HeroModel();
    }

    public override void Dispose()
    {
        LoginModel.Dispose();
        HeroModel.Dispose();
    }

    #region Events

    private readonly TypeEventSystem _dataEventSystem = new TypeEventSystem();

    public IUnRegister AddListener<T>(Action<T> action)
    {
        return _dataEventSystem.Register<T>(action);
    }

    public void RemoveListener<T>(Action<T> action)
    {
        _dataEventSystem.UnRegister<T>(action);
    }

    public void Broadcast<T>(T e)
    {
        _dataEventSystem.Send<T>(e);
    }

    #endregion
}