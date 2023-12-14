
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;


namespace cfg
{
public sealed partial class UIConfig : Luban.BeanBase
{
    public UIConfig(JSONNode _buf) 
    {
        { if(!_buf["id"].IsNumber) { throw new SerializationException(); }  Id = _buf["id"]; }
        { if(!_buf["name"].IsString) { throw new SerializationException(); }  Name = _buf["name"]; }
        { if(!_buf["path"].IsString) { throw new SerializationException(); }  Path = _buf["path"]; }
        { if(!_buf["layer"].IsNumber) { throw new SerializationException(); }  Layer = _buf["layer"]; }
        { if(!_buf["is_main"].IsBoolean) { throw new SerializationException(); }  IsMain = _buf["is_main"]; }
        { if(!_buf["is_full"].IsBoolean) { throw new SerializationException(); }  IsFull = _buf["is_full"]; }
        { if(!_buf["hiden_other"].IsBoolean) { throw new SerializationException(); }  HidenOther = _buf["hiden_other"]; }
        { if(!_buf["bg_alpha"].IsNumber) { throw new SerializationException(); }  BgAlpha = _buf["bg_alpha"]; }
        { if(!_buf["click_cross"].IsBoolean) { throw new SerializationException(); }  ClickCross = _buf["click_cross"]; }
        { if(!_buf["click_close"].IsBoolean) { throw new SerializationException(); }  ClickClose = _buf["click_close"]; }
    }

    public static UIConfig DeserializeUIConfig(JSONNode _buf)
    {
        return new UIConfig(_buf);
    }

    /// <summary>
    /// id
    /// </summary>
    public readonly int Id;
    /// <summary>
    /// 名字
    /// </summary>
    public readonly string Name;
    /// <summary>
    /// 路径
    /// </summary>
    public readonly string Path;
    /// <summary>
    /// 层级
    /// </summary>
    public readonly int Layer;
    /// <summary>
    /// 是否系统主面板
    /// </summary>
    public readonly bool IsMain;
    /// <summary>
    /// 是否全屏面板
    /// </summary>
    public readonly bool IsFull;
    /// <summary>
    /// 隐藏其他
    /// </summary>
    public readonly bool HidenOther;
    /// <summary>
    /// 背景颜色，预置几个透明度
    /// </summary>
    public readonly float BgAlpha;
    /// <summary>
    /// 点击穿透
    /// </summary>
    public readonly bool ClickCross;
    /// <summary>
    /// 点击任意地方关闭
    /// </summary>
    public readonly bool ClickClose;
   
    public const int __ID__ = 202324726;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
        
        
        
        
        
        
        
        
    }

    public override string ToString()
    {
        return "{ "
        + "id:" + Id + ","
        + "name:" + Name + ","
        + "path:" + Path + ","
        + "layer:" + Layer + ","
        + "isMain:" + IsMain + ","
        + "isFull:" + IsFull + ","
        + "hidenOther:" + HidenOther + ","
        + "bgAlpha:" + BgAlpha + ","
        + "clickCross:" + ClickCross + ","
        + "clickClose:" + ClickClose + ","
        + "}";
    }
}

}
