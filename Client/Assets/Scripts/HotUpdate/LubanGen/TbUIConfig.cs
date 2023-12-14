
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
public partial class TbUIConfig
{
    private readonly System.Collections.Generic.Dictionary<int, UIConfig> _dataMap;
    private readonly System.Collections.Generic.List<UIConfig> _dataList;
    
    public TbUIConfig(JSONNode _buf)
    {
        _dataMap = new System.Collections.Generic.Dictionary<int, UIConfig>();
        _dataList = new System.Collections.Generic.List<UIConfig>();
        
        foreach(JSONNode _ele in _buf.Children)
        {
            UIConfig _v;
            { if(!_ele.IsObject) { throw new SerializationException(); }  _v = UIConfig.DeserializeUIConfig(_ele);  }
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
    }

    public System.Collections.Generic.Dictionary<int, UIConfig> DataMap => _dataMap;
    public System.Collections.Generic.List<UIConfig> DataList => _dataList;

    public UIConfig GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public UIConfig Get(int key) => _dataMap[key];
    public UIConfig this[int key] => _dataMap[key];

    public void ResolveRef(Tables tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
    }

}

}