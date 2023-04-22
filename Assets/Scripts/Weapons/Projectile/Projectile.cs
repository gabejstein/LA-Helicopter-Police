using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float LifeTime = 3f;
    float lastTime;
    protected int damage = 10;
    protected bool friendlyFire = false; //So that we know if this came from the player or an ally.
    protected Transform _transform;
    protected Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _transform = this.transform;
    }

    private void OnEnable()
    {
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTime < LifeTime) return;

        gameObject.SetActive(false);

    }

    public void Launch(int damage, Vector3 position, Quaternion rotation, Vector3 forceDirection, bool isFriendly = false)
    {
        this.damage = damage;
        this.friendlyFire = isFriendly;
        _transform.position = position;
        _transform.rotation = rotation;

        rb.velocity = Vector3.zero;
        rb.AddForce(forceDirection, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable target = other.GetComponent<IDamageable>();
        if(target!=null)
        {
            target.GetHit(damage);
        }

        Impact();
    }

    protected virtual void Impact()
    {
        GameObject impactEffect = ObjectPool.singleton.GetObject("ImpactEffect");
        impactEffect.transform.position = transform.position;
        impactEffect.transform.rotation = transform.rotation;
        gameObject.SetActive(false);
    }
}
