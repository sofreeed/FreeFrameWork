using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class BaseWDCluster : BaseWDUnit
{
    private float _zoomHeightBegin = 30;
    private float _zoomHeightEnd = 15;
    private float _zoomSizeMin = 1;
    private float _zoomSizeMax = 3;

    private const float _CITY_TOWN_DISPLAY_HEIGHT = 0;


    private List<BaseCTUnit> _cityUnitList = new();


    public override void Init(EWDUnit type, float x, float y)
    {
        base.Init(type, x, y);

        //_zoomBeginCameraHeight = zoomBeginHeight;
        //_zoomEndCameraHeight = zoomEndHeight;
        //_zoomCitySize = zoomSize;
    }

    protected override void Update()
    {
        if (_lastWorldLod != EWorldLod.Level0)
            return;

        float cameraHeight = WorldMgr.Instance.Camera.transform.position.y;

        //城墙出现
        bool isTownView = !(cameraHeight > _CITY_TOWN_DISPLAY_HEIGHT);

        if (cameraHeight > _zoomHeightBegin)
        {
            //正常-高度还没到
            return;
        }

        //建筑缩放
        foreach (BaseCTUnit unit in _cityUnitList)
        {
            float currZoomHeight = _zoomHeightBegin - cameraHeight;
            float totalZoomHeight = _zoomHeightBegin - _zoomHeightEnd;
            float t = currZoomHeight / totalZoomHeight;
            float currZoom = Mathf.Lerp(_zoomSizeMax, _zoomSizeMin, t);
            unit.transform.localScale = new Vector3(currZoom, currZoom, currZoom);
        }

        //碰撞检测和平移
    }
}