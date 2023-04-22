using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC_Weapon_Base : MonoBehaviour
{
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected int shotDamage = 20;
    [SerializeField] protected string projectiblePrefabID = "Bullet";
    [SerializeField] protected float accuracyJitter = 0.5f;

    [SerializeField] protected AudioClip shotSFX = null;

    [SerializeField] protected Transform firePoint = null;

    [SerializeField] protected AudioSource aSource;

    public float GetFireRate() { return fireRate; }
    public virtual void Fire(Vector3 targetPosition)
    {
        GameObject bullet = ObjectPool.singleton.GetObject(projectiblePrefabID);

        if (bullet == null) { Debug.Log("Could not get bullet"); return; }

        float fireForce = 150f;

        Vector3 randDirection = new Vector3(Random.Range(accuracyJitter, -accuracyJitter), Random.Range(accuracyJitter, -accuracyJitter), Random.Range(accuracyJitter,-accuracyJitter));

        Vector3 fireDirection = (targetPosition - firePoint.position).normalized;
        Vector3 fireVector = fireDirection * fireForce;
        fireVector += randDirection;

        Projectile projectile = bullet.GetComponent<Projectile>();

        //TODO: Set the rotation to something facing the target.
        projectile.Launch(shotDamage, firePoint.position, Quaternion.LookRotation(targetPosition-firePoint.position), fireVector);

        if (shotSFX != null) aSource.PlayOneShot(shotSFX);
    }
}
