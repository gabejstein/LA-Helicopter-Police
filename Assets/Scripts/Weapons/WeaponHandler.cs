using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS_Helicopter
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] Weapon[] weapons;

        AmmoHolder ammoHolder;

        [SerializeField] Transform ShootingTarget;
        [SerializeField] RectTransform Crosshair;

        Vector3 CrosshairPoint = new Vector3(0, 0, 5f);

        private void Start()
        {
            ammoHolder = GetComponent<AmmoHolder>();

            if (weapons != null && ammoHolder!=null)
            {
                for (int i = 0; i < weapons.Length; i++)
                {
                    weapons[i].SetAmmoHolder(ammoHolder);
                }
            }

           
        }

        public void UpdateWeapons(InputManager inputManager)
        {
            if (weapons == null) return;

            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].UpdateWeapon(inputManager);
            }
            
        }

        private void LateUpdate()
        {
            //I put it in here to avoid jittering.
            UpdateCrossHair();
        }

        //TO DO: Move to its own specialized UI script.
        void UpdateCrossHair()
        {
            //Snaps the crosshair UI sprite to the targeting position
            Vector3 screenCrosshair = Camera.main.WorldToScreenPoint(ShootingTarget.position);
            Crosshair.position = screenCrosshair;
        }

    }
}


