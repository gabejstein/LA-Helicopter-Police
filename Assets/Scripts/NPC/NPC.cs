using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GS_Helicopter
{
   

   [RequireComponent(typeof(AudioSource))]
    public class NPC : MonoBehaviour, IDamageable
    {
        public Action OnDestroyed;
        public static Action OnDestroyedEnemy;

        [Header("General Info")]
        [SerializeField] protected string id;
        [SerializeField] protected int health = 100;
        DamageFlash damageFlash;

        [Header("AI")]
        StateMachine stateMachine;

        [Header("Weapon Data")]
        [SerializeField] protected int damagePower;
        [SerializeField] protected Transform firePoint;    
        public AudioClip weaponSFX;
        protected AudioSource aSource;

        // Start is called before the first frame update
        void Start()
        {
            stateMachine = GetComponent<StateMachine>();
            aSource = GetComponent<AudioSource>();
            damageFlash = GetComponent<DamageFlash>();
            
        }

        void Update()
        {
            if(stateMachine!=null)
                stateMachine.UpdateFSM();
        }

        public virtual void FireShot()
        {
            GameObject bullet = ObjectPool.singleton.GetObject("Bullet");
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = firePoint.transform.rotation;

            float fireForce = 150f;

            //Vector3 randDirection = new Vector3(Random.Range(0.5f, -0.5f), Random.Range(0.5f, -0.5f), Random.Range(0.5f, -0.5f));

            Vector3 fireVector = firePoint.forward * fireForce;
            
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
            bulletRB.velocity = Vector3.zero;
            bulletRB.AddForce(fireVector, ForceMode.Impulse);

            if(weaponSFX!=null) aSource.PlayOneShot(weaponSFX);
        }


        public void GetHit(int damage)
        {
            health -= damage;
            //Debug.Log("Enemy Health :" + health);
            damageFlash.StartFlash();

            if (health <= 0)
                Die();
        }

        void Die()
        {
            OnDestroyed?.Invoke();

            OnDestroyedEnemy?.Invoke();

            GameObject explosion = ObjectPool.singleton.GetObject("Explosion");
            explosion.transform.position = transform.position;
            
            damageFlash.StopFlash();

            gameObject.SetActive(false);
            //TO DO: Replace with dead model and make a jump up animation.
        }

    }
}
