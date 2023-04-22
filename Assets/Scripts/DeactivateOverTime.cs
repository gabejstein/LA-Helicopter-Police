using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOverTime : MonoBehaviour
{
    public float lifeSpan = 1f;
    float lastTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTime >= lifeSpan)
        {
            this.gameObject.SetActive(false);
        }
            
    }

    private void OnEnable()
    {
        lastTime = Time.time;
    }
}
