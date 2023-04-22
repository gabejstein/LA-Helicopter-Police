using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS_Helicopter
{
    public class GattlingGun : Weapon
    {
        [SerializeField] Transform fireTarget;

        public override void Fire()
        {
            
            aSource.PlayOneShot(weaponSFX);

            Vector3 fireDirection = fireTarget.position - fireSpawnPoint.transform.position;

            fireDirection = Vector3.Normalize(fireDirection);

            GameObject bullet = ObjectPool.singleton.GetObject("Bullet");
            bullet.transform.position = fireSpawnPoint.transform.position;
            bullet.transform.rotation = fireSpawnPoint.transform.rotation;
            bullet.transform.LookAt(fireTarget.position);
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
            bulletRB.velocity = Vector3.zero;

            

            bulletRB.AddForce(fireDirection * 4000f);
           
        }
    }
}
