using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECTUnit
{
    Town = 0,
    Wall,
    Hospital,
}

/// <summary>
/// 相机达到一定高度，开始进入主城展开逻辑
/// </summary>
public class CityMgr : MonoBehaviour
{
    public GameObject UnitRoot;
    public GameObject Prefab;
    
    public BaseCTUnit[,] Cells;
    private Dictionary<ECTUnit, List<BaseCTUnit>> _tnUnitList;
    
    
    //private Map _map;

    void Start()
    {
        
    }
}