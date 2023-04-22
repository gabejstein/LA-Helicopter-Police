using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AILandMover : AIMover
{
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public override void SetDestination(Vector3 destination)
    {
        if (agent.isStopped) agent.isStopped = false;
        agent.SetDestination(destination);
    }

    public override void StopMovement()
    {
        agent.isStopped = true;
    }
}
