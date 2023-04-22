using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AMMO_TYPE
{
    GATTLING,
    H_MISSILE
}

[System.Serializable]
public class Ammo
{
    public int amount;
    public int maxAmount;
    public AMMO_TYPE ammoType;
}
