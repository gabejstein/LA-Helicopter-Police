using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIMover : MonoBehaviour
{
    protected Vector3 destination;

    public abstract void SetDestination(Vector3 destination);

    public abstract void StopMovement();
}
