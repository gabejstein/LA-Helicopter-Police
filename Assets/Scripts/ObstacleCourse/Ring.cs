using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public int orderNumber = 0;
    public RingCourse ringCourse;

    public float TimeBonus = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        Debug.Log("entered ring number: " + orderNumber);
        ringCourse.OnEnterRing(this);
    }
}
