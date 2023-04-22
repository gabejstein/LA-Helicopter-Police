using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the component that acts as the player/entity's ammo inventory.
public class AmmoHolder : MonoBehaviour
{
    public Ammo[] AmmoSlots;
    
    public int GetAmount(AMMO_TYPE ammoType)
    {
        return GetAmmoFromSlot(ammoType).amount;
    }

    public void ConsumeAmmo(AMMO_TYPE ammoType)
    {
        Ammo ammo = GetAmmoFromSlot(ammoType);


        if (--ammo.amount <= 0) ammo.amount = 0;
    }

    Ammo GetAmmoFromSlot(AMMO_TYPE ammoType)
    {
        foreach(Ammo ammo in AmmoSlots)
        {
            if (ammo.ammoType == ammoType)
                return ammo;
        }

        return null;
    }

    public void IncreaseAmmo(int amount, AMMO_TYPE ammoType)
    {
        Ammo ammoSlot = GetAmmoFromSlot(ammoType);
        //Debug.Log("Ammo increased");
        if (ammoSlot == null) return;
        
        ammoSlot.amount += amount;
        if (ammoSlot.amount > ammoSlot.maxAmount)
            ammoSlot.amount = ammoSlot.maxAmount;
    }
}
