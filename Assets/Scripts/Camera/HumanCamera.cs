using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCamera : MonoBehaviour
{
    Vector3 distanceFromPlayer;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        distanceFromPlayer = target.position-transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 displacement = target.position - distanceFromPlayer;
        transform.position = displacement;
        //transform.LookAt(target);
    }
}
