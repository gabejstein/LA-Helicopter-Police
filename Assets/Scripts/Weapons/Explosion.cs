using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS_Helicopter;

public class Explosion : MonoBehaviour
{
    int explosionDamage = 200; //TO DO: Allow this to be set by the firing weapon
    [SerializeField] AudioClip explosionSFX;
    bool hasInitialized = false;

    private void OnEnable()
    {
        if (hasInitialized)
            AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position);
        else
            hasInitialized = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable attackable = other.gameObject.GetComponent<IDamageable>();

       
        if (attackable!=null)
        {
            attackable.GetHit(explosionDamage);
            Debug.Log("was attacked with explosion");
        }
        
    }

}
