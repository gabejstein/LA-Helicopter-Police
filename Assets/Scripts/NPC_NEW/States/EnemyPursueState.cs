using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPursueState : GameState
{
    float maxPursueTime = 5f;
    float startTime = 0f;

    EnemyDetector detector;
    NavMeshAgent agent;
    

    public EnemyPursueState(GameObject context, float maxPursueTime):base(context)
    {
        this.stateName = "Pursue";

        this.maxPursueTime = maxPursueTime;
        detector = context.GetComponent<EnemyDetector>();
        agent = context.GetComponent<NavMeshAgent>();
    }

    public override void Init()
    {
        startTime = Time.time;
        agent.SetDestination(detector.GetPlayerPosition());
        agent.isStopped = false;
    }

    public override void Update()
    {
        if(Time.time-startTime > maxPursueTime)
        {
            context.GetComponent<NPC_Base>().ChangeState("Patrol"); //TODO: Need to roll back a state instead of changing it.
            return;
        }

        if(detector.CanSeePlayer())
        {
            context.GetComponent<NPC_Base>().ChangeState("Attack");
            return;
        }

        agent.SetDestination(detector.GetPlayerPosition());
    }

    public override void Exit()
    {
        agent.isStopped = true;
    }


}
