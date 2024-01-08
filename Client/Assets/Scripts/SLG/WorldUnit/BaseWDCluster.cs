using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class BaseWDCluster : BaseWDUnit
{
    private List<BaseCTUnit> _cityUnitList = new();

    private float _zoomBeginCameraHeight = 0;
    private float _zoomEndCameraHeight = 0;
    private float _zoomCitySize = 1;
    private float _zoomWorldSize = 3;

    private float _cityTownDisplayHeight = 0;

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

        float camerHeight = WorldMgr.Instance.Camera.transform.position.y;

        //城墙出现
        bool isTownView = !(camerHeight > _cityTownDisplayHeight);

        if (camerHeight > _zoomBeginCameraHeight)
        {
            //正常-高度还没到
            return;
        }

        //建筑缩放
        foreach (BaseCTUnit unit in _cityUnitList)
        {
            float currZoomHeight = _zoomBeginCameraHeight - camerHeight;
            float totalZoomHeight = _zoomBeginCameraHeight - _zoomEndCameraHeight;
            float t = currZoomHeight / totalZoomHeight;
            float currZoom = Mathf.Lerp(_zoomWorldSize, _zoomCitySize, t);
            unit.transform.localScale = new Vector3(currZoom, currZoom, currZoom);
        }

        //碰撞检测和平移
    }
}