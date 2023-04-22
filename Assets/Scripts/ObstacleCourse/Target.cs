using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Target : MonoBehaviour, IDamageable
{
    public string id;
    public Action WasDestroyed;
   
    public void GetHit(int damage)
    {
        string message = "DestroyedTarget" + id;
        Debug.Log(message);
        WasDestroyed.Invoke();
        gameObject.SetActive(false);
    }
}
