using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : GameState
{
    Transform[] path;
    //AILandMover mover;
    NavMeshAgent mover;
    EnemyDetector detector;

    int currentWaypoint = 0;
   public EnemyPatrolState(GameObject context, Transform[] path):base(context)
    {
        this.stateName = "Patrol";
        this.path = path;

        this.mover = context.GetComponent<NavMeshAgent>();
        this.detector = context.GetComponent<EnemyDetector>();
    }

    public override void Init()
    {
        if(path==null)
        {
            context.GetComponent<NPC_Base>().ChangeState("Idle");
            return;
        }

        mover.SetDestination(path[currentWaypoint].position);
        mover.isStopped = false;
    }

    public override void Update()
    {
        if(detector.CanSeePlayer())
        {
            context.GetComponent<NPC_Base>().ChangeState("Attack");
            return;
        }

        if(hasArrived())
        {
            currentWaypoint++;

            if(currentWaypoint >= path.Length)
                currentWaypoint = 0;

            mover.SetDestination(path[currentWaypoint].position);
        }
    }

    public override void Exit()
    {
        mover.isStopped = true;
    }

    bool hasArrived()
    {
        float distance = Vector3.Distance(context.transform.position,path[currentWaypoint].position);

        if(distance < 2f)
        {
            return true;
        }

        return false;
    }
   
}
