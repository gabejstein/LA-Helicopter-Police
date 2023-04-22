using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS_Helicopter
{
    public class Player : MonoBehaviour,IDamageable
    {
        WeaponHandler weaponHandler;
        HelicopterController flightSystem;
        InputManager inputManager;

        FuelTank fuelTank;
        LiftingCable liftCable;

        [SerializeField] int health = 500;
        [SerializeField] int maxHealth = 500;
        [SerializeField] bool GodMode = false;
        bool isDead = false;

        DamageFlash damageFlash;

        // Start is called before the first frame update
        void Start()
        {
            weaponHandler = GetComponent<WeaponHandler>();
            flightSystem = GetComponent<HelicopterController>();
            inputManager = GetComponent<InputManager>();
            liftCable = GetComponent<LiftingCable>();
            fuelTank = GetComponent<FuelTank>();
            damageFlash = GetComponent<DamageFlash>();

            
        }

        // Update is called once per frame
        void Update()
        {
            fuelTank.UpdateFuel();

            if(fuelTank.GetFuelAmount()<=0)
            {
                flightSystem.RunOutOfFuel();
            }

            weaponHandler.UpdateWeapons(inputManager);
            flightSystem.UpdateFlightSystem(inputManager);
            liftCable.UpdateLiftCable();
        }

        public void GetHit(int damage)
        {
            damageFlash.StartFlash();

            if (isDead || GodMode) return;

            health -= damage;

            if (health <= 0)
            {
                health = 0;
                Die();
            }
                
        }

        void Die()
        {
            //TODO: Mission Failure
            GameObject explosion = ObjectPool.singleton.GetObject("Explosion");
            damageFlash.StopFlash();

            explosion.transform.position = transform.position;

            isDead = true;

            gameObject.SetActive(false);

            //Restart level or go to menu.
        }

        public int GetHealth() { return health; }

        public int GetMaxHealth() { return maxHealth; }

        public int GetFuelAmount() { return fuelTank.GetFuelAmount(); }

        public int GetMaxFuelAmount() { return fuelTank.GetMaxFuelAmount(); }
    }
}