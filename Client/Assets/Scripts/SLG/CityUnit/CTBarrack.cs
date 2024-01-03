using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECTBarrack
{
    Foot = 0,
    Archer,
    Rider
}

public class CTBarrack : BaseCTUnit
{
    private ECTBarrack _ectBarrack;
}