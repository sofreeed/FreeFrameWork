using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDUnitLodPrefab
{
    public EWorldLod EWorldLod;
    public GameObject Prefab;
}

public abstract class BaseWDUnit : MonoBehaviour
{
    public EWDUnit UnitType { get; set; }
    public Vector2 Position { get; set; }

    protected Dictionary<EWorldLod, Transform> _lodPrefabDict = new();

    protected EWorldLod _lastWorldLod = EWorldLod.Level0;

    protected virtual void Awake()
    {
    }

    public virtual void Init(EWDUnit type, float x, float z)
    {
        UnitType = type;
        Position = transform.Position(x, 0.5f, z).Position();

        Transform lod0 = transform.Find("LOD0");
        Transform lod1 = transform.Find("LOD1");
        Transform lod2 = transform.Find("LOD2");

        _lodPrefabDict.Add(EWorldLod.Level0, lod0);
        _lodPrefabDict.Add(EWorldLod.Level1, lod1);
        _lodPrefabDict.Add(EWorldLod.Level2, lod2);
        
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
        SetAllLodUnActive();
        if (_lodPrefabDict.TryGetValue(lodLevel, out var tran))
        {
            tran.gameObject.SetActive(true);
        }
    }

    private void SetAllLodUnActive()
    {
        foreach (var kv in _lodPrefabDict)
        {
            kv.Value.gameObject.SetActive(false);
        }
    }
}