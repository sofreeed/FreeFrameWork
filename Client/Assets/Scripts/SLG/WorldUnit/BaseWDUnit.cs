using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWDUnit : MonoBehaviour
{
    public EWDUnit UnitType { get; set; }
    public Vector2 Position { get; set; }

    protected EWorldLod _lastWorldLod = EWorldLod.Level0;
    protected Dictionary<EWorldLod, Transform> _lodPrefabDict = new();

    
    protected virtual void Awake()
    {
    }

    public virtual void Init(EWDUnit type, float x, float z)
    {
        UnitType = type;
        Position = transform.Position(x, 0.5f, z).Position();

        _lodPrefabDict.Add(EWorldLod.Level0, transform.Find("LOD0"));
        _lodPrefabDict.Add(EWorldLod.Level1, transform.Find("LOD1"));
        _lodPrefabDict.Add(EWorldLod.Level2, transform.Find("LOD2"));
        
        SetLodActive(_lastWorldLod);
    }

    protected virtual void Update()
    {
    }

    public virtual void OnLodLevelChange(EWorldLod eWorldLod)
    {
        if (_lastWorldLod == eWorldLod)
            return;

        _lastWorldLod = eWorldLod;
        SetLodActive(_lastWorldLod);
    }

    private void SetLodActive(EWorldLod lodLevel)
    {
        SetAllLodEnable();
        if (_lodPrefabDict.TryGetValue(lodLevel, out var tran))
        {
            tran.gameObject.SetActive(true);
        }
    }

    private void SetAllLodEnable()
    {
        foreach (var kv in _lodPrefabDict)
        {
            kv.Value.gameObject.SetActive(false);
        }
    }
}