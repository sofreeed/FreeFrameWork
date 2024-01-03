using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECTAsset
{
    Wood = 0,
    Food,
    Stone,
    Gold
}


public class CTAsset : BaseCTUnit
{
    private ECTAsset _ectAsset;
}