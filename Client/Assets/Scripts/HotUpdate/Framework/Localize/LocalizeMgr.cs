using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using cfg;

public enum EGameLanguage
{
    zh_CN = 0,
    zh_TW = 1,
    en = 2,
}

public static class LocalizeExtend
{
    public static string Localize(this string key)
    {
        return LocalizeMgr.Instance.GetText(key);
    }
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
        //1.如果本地有缓存直接返回
        //2.策划默认设置
        //3.根据系统语言判断
        SystemLanguage systemLanguage = Application.systemLanguage;
        switch (systemLanguage)
        {
            case SystemLanguage.Chinese:
            case SystemLanguage.ChineseSimplified:
                GameLanguage = EGameLanguage.zh_CN;
                break;
            case SystemLanguage.ChineseTraditional:
                GameLanguage = EGameLanguage.zh_TW;
                break;
            case SystemLanguage.English:
                GameLanguage = EGameLanguage.en;
                break;
            default:
                GameLanguage = EGameLanguage.zh_CN;
                break;
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
    
    public string GetText(string key)
    {
        if (GameLanguage == EGameLanguage.zh_CN)
        {
            return LocalizeConfigCNCategory[key].TextCn;
        }
        else if (GameLanguage == EGameLanguage.zh_TW)
        {
            return LocalizeConfigENCategory[key].TextEn;
        }
        else if (GameLanguage == EGameLanguage.en)
        {
            return LocalizeConfigTWCategory[key].TextTw;
        }
        else
        {
            return "";
        }
    }

    
}