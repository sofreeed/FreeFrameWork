using System.Collections.Generic;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public enum EWorldLod
{
    Level0 = 0, //建筑可以进行缩放
    Level1, //所有单位替换图标
    Level2, //建筑、资源、野怪消失
    Level3, //数目消失
    Level4, //山体换建模资源
}

public struct WorldLod
{
    public EWorldLod LodLevel;
    public float CameraHeight;

    public WorldLod(EWorldLod lod, float cameraHeight)
    {
        LodLevel = lod;
        CameraHeight = cameraHeight;
    }
}

public enum EWDUnit
{
    MainCity = 0,
    Building,
    Asset,
    Army,
}

public class WorldMgr : MonoBehaviour
{
    public static WorldMgr Instance;

    public const int SIZE_X = 24;
    public const int SIZE_Z = 24;

    public GameObject UnitRoot;
    public GameObject CameraRoot;
    public GameObject CameraGo;
    public Camera Camera;

    private readonly List<BaseWDUnit> _unitList = new(256); 
    private readonly Dictionary<EWDUnit, List<BaseWDUnit>> _unitDict = new(256); 

    private List<WorldLod> _lodSetting = new(8);

    //private Map _map;

    private const float _ZOOM_MAX = 128;
    private const float _ZOOM_MIN = 10;
    private const float _ZOOM_SPEED = 4;
    private const float _TOUCH_ZOOM_SPEED = 0.2f; 
    private float _zoomCurrent = 30f;

    private Vector2 _mousePosOld = Vector2.zero;
    private Vector3 _vCamRootPosOld = Vector3.zero;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _lodSetting.Add(new WorldLod(EWorldLod.Level0, 35));
        _lodSetting.Add(new WorldLod(EWorldLod.Level1, 45));
        _lodSetting.Add(new WorldLod(EWorldLod.Level2, 60));

        //构建随机数据
        //CreateUnit(EWDUnit.MainCity, Random.Range(0, SizeX), Random.Range(0, SizeX));

        CreateUnit(EWDUnit.Building, Random.Range(-24, SIZE_X), Random.Range(-24, SIZE_Z));
        CreateUnit(EWDUnit.Building, Random.Range(-24, SIZE_X), Random.Range(-24, SIZE_Z));
        CreateUnit(EWDUnit.Building, Random.Range(-24, SIZE_X), Random.Range(-24, SIZE_Z));
        CreateUnit(EWDUnit.Building, Random.Range(-24, SIZE_X), Random.Range(-24, SIZE_Z));
        CreateUnit(EWDUnit.Building, Random.Range(-24, SIZE_X), Random.Range(-24, SIZE_Z));

        //CreateUnit(EWDUnit.Asset, Random.Range(0, SizeX), Random.Range(0, SizeX));
        //CreateUnit(EWDUnit.Asset, Random.Range(0, SizeX), Random.Range(0, SizeX));
        //CreateUnit(EWDUnit.Asset, Random.Range(0, SizeX), Random.Range(0, SizeX));
    }

    void OnEnable()
    {
        EasyTouch.On_SwipeStart += OnSwipeStart;
        EasyTouch.On_Swipe += OnSwipe;
        EasyTouch.On_SwipeEnd += OnSwipeEnd;
    }

    void OnDisable()
    {
        EasyTouch.On_Swipe -= OnSwipe;
        EasyTouch.On_SwipeStart -= OnSwipeStart;
        EasyTouch.On_SwipeEnd -= OnSwipeEnd;
    }

    private void CreateUnit(EWDUnit type, float x, float y)
    {
        GameObject prefab = Resources.Load<GameObject>("Unit");
        BaseWDUnit unit = prefab.InstantiateWithParent(UnitRoot.transform).GetComponent<BaseWDUnit>();
        unit.Init(type, x, y);

        _unitList.Add(unit);
        if (_unitDict.TryGetValue(type, out var value))
        {
            value.Add(unit);
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
        //相机缩放-鼠标
        if (true)
        {
            _zoomCurrent -= Input.GetAxis("Mouse ScrollWheel") * _ZOOM_SPEED;
            _zoomCurrent = Mathf.Clamp(_zoomCurrent, _ZOOM_MIN, _ZOOM_MAX);
            CameraGo.transform.localPosition = new Vector3(0, 0, -_zoomCurrent);
        }

        //相机缩放-触屏
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            _zoomCurrent += deltaMagnitudeDiff * _TOUCH_ZOOM_SPEED;
            _zoomCurrent = Mathf.Clamp(_zoomCurrent, _ZOOM_MIN, _ZOOM_MAX);
            CameraGo.transform.localPosition = new Vector3(0, 0, -_zoomCurrent);
        }

        for (int i = 0; i < _lodSetting.Count; i++)
        {
            WorldLod lod = _lodSetting[i];
            if (_zoomCurrent < lod.CameraHeight)
            {
                foreach (var unit in _unitList)
                {
                    unit.OnLodLevelChange(lod.LodLevel);
                }

                break;
            }
        }
    }


    private void OnSwipeStart(Gesture gesture)
    {
        _vCamRootPosOld = CameraRoot.transform.position;
        _mousePosOld = gesture.position;
    }

    private void OnSwipe(Gesture gesture)
    {
        float f = _zoomCurrent / 34; //相机越高滑动越快
        Vector2 vDelta = (gesture.position - _mousePosOld) * 0.008f * f;
        CameraMove(vDelta);
    }

    private void OnSwipeEnd(Gesture gesture)
    {
    }

    private void CameraMove(Vector2 vDelta)
    {
        //Vector3 vDelta = (Input.mousePosition - _mousePosOld) * 0.008f;
        Vector3 vForward = CameraRoot.transform.forward;
        vForward.y = 0.0f;
        vForward.Normalize();
        Vector3 vRight = CameraRoot.transform.right;
        vRight.y = 0.0f;
        vRight.Normalize();
        Vector3 vMove = -vForward * vDelta.y + -vRight * vDelta.x;
        CameraRoot.transform.position = _vCamRootPosOld + vMove;
    }
}