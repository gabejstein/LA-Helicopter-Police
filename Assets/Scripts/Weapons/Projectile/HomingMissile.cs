using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : Projectile
{
    Transform target;
    float Speed = 60f;

    // Update is called once per frame
    void Update()
    {
        if(target!=null)
        {
            Vector3 direction = target.transform.position - this.transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 2f);
            transform.Translate(0, 0, Speed * Time.deltaTime);
        }
 
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject explosion = ObjectPool.singleton.GetObject("Explosion");
        explosion.transform.position = transform.position;
        explosion.transform.rotation = transform.rotation;

        target = null;

        gameObject.SetActive(false);
    }
}
