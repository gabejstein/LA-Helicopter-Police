using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    public Transform target;

    [Range(0,1)]
    public float lValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LetsLerp(Vector3.zero,new Vector3(0,0,10f));
    }

    void LetsLerp(Vector3 start, Vector3 end)
    {
        target.position = Vector3.Lerp(start, end, lValue);
    }
}
