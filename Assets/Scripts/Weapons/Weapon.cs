using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS_Helicopter
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected GameObject fireSpawnPoint;
        [SerializeField] protected GameObject heliBody;
        [SerializeField] protected AudioClip weaponSFX;
        [SerializeField] protected AudioClip noAmmoSFX;

        public AMMO_TYPE ammoTYPE;
        protected AmmoHolder ammoHolder; //reference to ammo inventory on player

        protected float fireRate = 0.1f;
        protected float lastFired = 0f;

        protected AudioSource aSource;

        

        private void Start()
        {
            aSource = GetComponent<AudioSource>();
            Initialize();
        }

        public virtual void Initialize() 
        {
            
        }

        public void SetAmmoHolder(AmmoHolder _ammoHolder)
        {
            ammoHolder = _ammoHolder;
        }

        public virtual void UpdateWeapon(InputManager inputManager)
        {
            if (Time.time - lastFired < fireRate) return;

            if (inputManager.FireButtonDown())
            {
                if(ammoHolder.GetAmount(ammoTYPE)>0)
                {
                    Fire();
                    ammoHolder.ConsumeAmmo(ammoTYPE);
                }
                else
                {
                    //TODO: Play bullet empty sound effect.
                    aSource.PlayOneShot(noAmmoSFX);
                }
                    
            }
                

            lastFired = Time.time;
        }

        public virtual void Fire()
        {
            
        }
    }
}
