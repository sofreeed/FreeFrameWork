using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCTUnit : MonoBehaviour
{
    public ECTUnit UnitType { get; set; }
    public bool IsDisplay { get; set; }
    public bool CollideLevel { set; get; }

    public int TileX;
    public int TileY;

    public int Wdith;
    public int Height;

    public int PosX;
    public int PosY;

    void Start()
    {
    }

    public virtual void Init(int x, int y, int wdith, int height)
    {
        TileX = x;
        TileY = y;
        Wdith = wdith;
        Height = height;
    }

    //计算obj坐标
    public void SetPosition()
    {
        transform.localPosition = Vector3.one;
    }

    public void GetRectSize()
    {
    }

    void Update()
    {
    }
}