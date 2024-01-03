using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWDArmy
{
    Monster = 0,
    Army
}

public class WDArmy : BaseWDCluster
{
    private EWDArmy _wdArmyType;

    public override void Init(EWDUnit type, float x, float y)
    {
        base.Init(type, x, y);
    }
}