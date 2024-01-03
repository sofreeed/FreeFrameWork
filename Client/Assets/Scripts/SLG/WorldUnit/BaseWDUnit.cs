using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWDUnit : MonoBehaviour
{
    public EWDUnit UnitType { get; set; }
    public Vector2 Position { get; set; }

    protected virtual void Start()
    {
    }

    public virtual void Init(EWDUnit type, float x, float y)
    {
        UnitType = type;
        Position = new Vector2(x, y);
    }

    protected virtual void Update()
    {
        
    }

    public virtual void OnLodLevelChange(EWorldLod eWorldLod)
    {
    }
}