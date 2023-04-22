using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS_Helicopter
{
    public class HMissileLauncher : Weapon
    {
        NPC target;
        [SerializeField] RectTransform targetSquare;

        [SerializeField] float playerViewCone = 120f; //How wide of an angle the weapon can acquire a target from
        [SerializeField] float maxScanDistance = 500f; //how close the target has to be to be acquired.

        public Transform aimingPoint; //the position where a raycast will start to search for targets
        public AudioClip targetAquiredSFX;

        float lastTargetAquired;

        public override void Initialize()
        {
            
        }

        public override void UpdateWeapon(InputManager inputManager)
        {
            if (inputManager.LockOnButtonDown())
                AquireTarget();

            if (inputManager.FireMissileButtonDown()) //really need to refactor this so its making better use of inheritance.
            {
                if (Time.time - lastFired > 0.5f)
                {
                    if (ammoHolder.GetAmount(ammoTYPE) > 0)
                    {
                        Fire();
                        ammoHolder.ConsumeAmmo(ammoTYPE);

                    }
                    else
                    {
                        aSource.PlayOneShot(noAmmoSFX);
                    }

                    lastFired = Time.time;
                }
            }

            HandleTargetSprite();
        }

        //TO DO: Move to its own specialized UI script.
        private void HandleTargetSprite()
        {
            //Snaps the crosshair UI sprite to the targeting position. TO DO: Move to a UI manager.
            if (target != null)
            {
                Vector3 screenCrosshair = Camera.main.WorldToScreenPoint(target.transform.position);
                targetSquare.position = screenCrosshair;
            }
        }

        public override void Fire()
        {

            
           
            aSource.PlayOneShot(weaponSFX);

            GameObject missile = ObjectPool.singleton.GetObject("Missile");

            missile.transform.position = fireSpawnPoint.transform.position;
            missile.transform.rotation = fireSpawnPoint.transform.rotation;

            Rigidbody rb = missile.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;

            if (target == null)
            {
                rb.useGravity = true;
                rb.AddForce(heliBody.transform.forward * 4000f);
                Debug.Log("Firing without target");
            }
            else
            {
                rb.useGravity = false;
                missile.GetComponent<HomingMissile>().SetTarget(target.transform);
            }


            
        }

        private void AquireTarget()
        {
            if (Time.time - lastTargetAquired < 1f) return;

            //removes callback from current target. Just so we don't end up calling multiple callbacks from multiple targets.
            if (target != null) target.OnDestroyed -= UnSetTarget; 

            target = GetEnemyTarget();

            if (target != null)
            {
                targetSquare.gameObject.SetActive(true);
                aSource.PlayOneShot(targetAquiredSFX);
                target.OnDestroyed += UnSetTarget;
            }
            else
            {
                targetSquare.gameObject.SetActive(false);
            }

            lastTargetAquired = Time.time;
        }

        //to be used with the auto-aim system or targetting systems.
        private NPC GetEnemyTarget()
        {
            NPC[] enemies = FindObjectOfType<EnemyController>().GetAllEnemies();
            int highest = -1;

            Vector3 enemyDirectionA = enemies[0].transform.position - transform.position;
            float angleA = Vector3.Angle(enemyDirectionA, transform.forward);

            for (int i = 0; i < enemies.Length; i++)
            {
                if (!enemies[i].gameObject.activeInHierarchy) continue;

                Vector3 enemyDirectionB = enemies[i].transform.position - transform.position;
                float angleB = Vector3.Angle(enemyDirectionB, transform.forward);

                if (angleB < playerViewCone && angleB <= angleA)
                {
                    highest = i;
                    angleA = angleB;
                }

            }

            if (highest == -1) return null;

            NPC mostFrontal = enemies[highest];

            return mostFrontal;
        }

        private void UnSetTarget()
        {   
            target.OnDestroyed -= UnSetTarget;
            target = null;
            targetSquare.gameObject.SetActive(false);
        }
    }
}
