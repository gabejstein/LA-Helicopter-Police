using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : GameState
{
    EnemyDetector detector;
    public EnemyIdleState(GameObject context, EnemyDetector detector):base(context)
    {
        this.stateName = "Idle";

        this.detector = detector;
        
    }
    public override void Init()
    {
        
    }

    public override void Update()
    {
        if (detector.CanSeePlayer())
        {
            context.GetComponent<NPC_Base>().ChangeState("Attack");
        }
    }
    public override void Exit()
    {
        
    }


}
