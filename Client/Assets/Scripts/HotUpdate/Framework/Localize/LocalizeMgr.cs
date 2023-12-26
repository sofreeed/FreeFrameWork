using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using cfg;

public enum EGameLanguage
{
    zh = 0,
    en,
    zh_TW
}

public class LocalizeMgr : Singleton<LocalizeMgr>
{
    public EGameLanguage GameLanguage { get; set; }

    public LocalizeConfig_CNCategory LocalizeConfigCNCategory { get; set; }
    public LocalizeConfig_ENCategory LocalizeConfigENCategory { get; set; }
    public LocalizeConfig_TWCategory LocalizeConfigTWCategory { get; set; }

    public static Action OnLanguageChange;
    
    private LocalizeMgr()
    {
        
    }

    public override void Init()
    {
        SetLanguage();
    }

    public override void Dispose()
    {
        LocalizeConfigCNCategory = null;
        LocalizeConfigENCategory = null;
        LocalizeConfigTWCategory = null;
        OnLanguageChange = null;
    }

    private void SetLanguage()
    {
        GameLanguage = EGameLanguage.zh;

        SystemLanguage systemLanguage = Application.systemLanguage;
        switch (systemLanguage)
        {
            case SystemLanguage.Chinese:
                GameLanguage = EGameLanguage.zh;
                break;
            case SystemLanguage.English:
                GameLanguage = EGameLanguage.en;
                break;
        }
    }

    private string GetText(string key)
    {
        if (GameLanguage == EGameLanguage.zh)
        {
            return LocalizeConfigCNCategory[key].TextCn;
        }
        else if (GameLanguage == EGameLanguage.en)
        {
            return LocalizeConfigENCategory[key].TextEn;
        }
        else if (GameLanguage == EGameLanguage.zh_TW)
        {
            return LocalizeConfigTWCategory[key].TextTw;
        }
        else
        {
            return "";
        }
    }

    //动态切换语言时调用
    public void SwichLanguage(EGameLanguage language)
    {
        ReLoad();
        OnLanguageChange?.Invoke();
    }

    public void ReLoad()
    {
        //根据GameLanguage设置翻译表，当然Luban也不要加载全部翻译
        LocalizeConfigCNCategory = DataTableMgr.Tables.LocalizeConfigCNCategory;
        LocalizeConfigENCategory = DataTableMgr.Tables.LocalizeConfigENCategory;
        LocalizeConfigTWCategory = DataTableMgr.Tables.LocalizeConfigTWCategory;
    }
}