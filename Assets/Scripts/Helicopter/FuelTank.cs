using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    [SerializeField] int maxFuelAmount = 1000;
    [SerializeField] int depleteRatePerSecond = 1;
    int fuelAmount;

    float lastDepleteTime = 0f;

    //public Action OnTankEmpty();

    private void Start()
    {
        fuelAmount = maxFuelAmount;

        lastDepleteTime = Time.time;
    }

    public void UpdateFuel()
    {
        if(fuelAmount >0)
            Deplete();

    }

    void Deplete()
    {
        if(Time.time-lastDepleteTime>10f)
        {
            fuelAmount -= depleteRatePerSecond;
            lastDepleteTime = Time.time;
        }
        
        
        if(fuelAmount <= 0)
        {
            fuelAmount = 0;
        }
    }

    public void Refuel(int amount)
    {
        fuelAmount += amount;
        fuelAmount = Mathf.Clamp(fuelAmount, 0, maxFuelAmount);
    }

    public int GetFuelAmount()
    {
        return fuelAmount;
    }

    public int GetMaxFuelAmount()
    {
        return maxFuelAmount;
    }

}
