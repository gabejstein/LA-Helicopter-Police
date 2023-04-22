using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup
{
    AmmoHolder playerAmmoHolder;
    

    [SerializeField] int ammoAmount = 100;
    [SerializeField] AMMO_TYPE ammoType = AMMO_TYPE.GATTLING;

    protected override void Start()
    {
        base.Start();
        playerAmmoHolder = playerObject.GetComponent<AmmoHolder>();
        
    }
    public override void Process()
    {
        playerAmmoHolder.IncreaseAmmo(ammoAmount, ammoType);

       base.Process();
    }
}
