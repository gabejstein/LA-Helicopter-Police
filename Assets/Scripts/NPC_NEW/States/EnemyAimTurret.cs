using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimTurret : GameState
{
    Transform turret;
    Transform cannon;

    bool turretInPosition;

    float t = 0f;

    EnemyDetector detector;

   public EnemyAimTurret(GameObject context, Transform turret, Transform cannon):base(context)
    {
        this.turret = turret;
        this.cannon = cannon;

        detector = context.GetComponent<EnemyDetector>();
    }

    public override void Init()
    {
        t = 0f;
    }

    public override void Update()
    {
        if (!turretInPosition)
            TurnTurret();
        else
            TurnCannon();
    }

    public override void Exit()
    {
        
    }

    void TurnTurret()
    {
        Vector3 displacement = detector.GetPlayerPosition() - turret.position;

        //turret.rotation = Quaternion.Slerp();
    }

    void TurnCannon()
    {

    }


}
