using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public enum EWorldLod
{
    Level0 = 0, //建筑可以进行缩放
    Level1, //所有单位替换图标
    Level2, //建筑、资源、野怪消失
    Level3, //数目消失
    Level4, //山体换建模资源
}

public enum EWDUnit
{
    MainCity = 0,
    Building,
    Asset,
    Army,
}

/// <summary>
/// 1.相机随鼠标滑动调整高度
/// 2.随着相机高度调整，设置LOD
/// 
/// </summary>
public class WorldMgr : MonoBehaviour
{
    public static WorldMgr Instance;

    public static int SizeX = 24;
    public static int SizeZ = 24;

    public GameObject UnitRoot;
    public GameObject CameraRoot;
    public GameObject CameraGo;
    public Camera Camera;

    private readonly List<BaseWDUnit> _unitList = new(256); //根据可能的数量设置初始容量，避免自动扩容
    private readonly Dictionary<EWDUnit, List<BaseWDUnit>> _unitDict = new(256); //根据可能的数量设置初始容量，避免自动扩容

    private Dictionary<EWorldLod, float> _lodSetting;

    //private Map _map;

    private float zoomMax = 128;
    private float zoomMin = 16;
    private float zoomCurrent = 34f;
    private float zoomSpeed = 4;

    public float perspectiveZoomSpeed = 0.0001f; // The rate of change of the field of view in perspective mode.


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //_lodSetting = new Dictionary<LodLevel, float>(5);
        //_lodSetting.Add(LodLevel.World1, 300);
        //_lodSetting.Add(LodLevel.World2, 400);

        //构建随机数据
        //CreateUnit(EWDUnit.MainCity, Random.Range(0, SizeX), Random.Range(0, SizeX));

        CreateUnit(EWDUnit.Building, Random.Range(-24, SizeX), Random.Range(-24, SizeZ));
        CreateUnit(EWDUnit.Building, Random.Range(-24, SizeX), Random.Range(-24, SizeZ));
        CreateUnit(EWDUnit.Building, Random.Range(-24, SizeX), Random.Range(-24, SizeZ));
        CreateUnit(EWDUnit.Building, Random.Range(-24, SizeX), Random.Range(-24, SizeZ));
        CreateUnit(EWDUnit.Building, Random.Range(-24, SizeX), Random.Range(-24, SizeZ));

        //CreateUnit(EWDUnit.Asset, Random.Range(0, SizeX), Random.Range(0, SizeX));
        //CreateUnit(EWDUnit.Asset, Random.Range(0, SizeX), Random.Range(0, SizeX));
        //CreateUnit(EWDUnit.Asset, Random.Range(0, SizeX), Random.Range(0, SizeX));
    }

    private void CreateUnit(EWDUnit type, float x, float y)
    {
        GameObject prefab = Resources.Load<GameObject>("Unit");
        BaseWDUnit unit = prefab.InstantiateWithParent(UnitRoot.transform).GetComponent<BaseWDUnit>();
        unit.Init(type, x, y);

        _unitList.Add(unit);
        if (_unitDict.ContainsKey(type))
        {
            _unitDict[type].Add(unit);
        }
        else
        {
            List<BaseWDUnit> list = new();
            list.Add(unit);
            _unitDict.Add(type, list);
        }
    }

    void Update()
    {
        //TODO：调整相机高度，根据鼠标滚轮和触屏
        //TODO：处理拖动平移相机
        //TODO：处理建筑拾取、拖动和落城
        //TODO：判断当前Lod级别，并通知全部建筑
    }
}