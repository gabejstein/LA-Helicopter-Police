using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPickup : Pickup
{
    [SerializeField] int fuelAmount = 50;
    FuelTank fuelTank;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        fuelTank = playerObject.GetComponent<FuelTank>();
    }

    public override void Process()
    {
        fuelTank.Refuel(fuelAmount);
        base.Process();
    }
}
