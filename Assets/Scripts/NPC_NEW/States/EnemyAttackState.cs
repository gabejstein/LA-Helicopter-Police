using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : GameState
{
    EnemyDetector detector;
    TankCannon weapon;

    Transform turretTransform;
    Transform cannonTransform;
    
    Vector3 target;

    float lastFireTime = 0f;
    float fireRate;
    public EnemyAttackState(GameObject context, TankCannon weapon, EnemyDetector detector):base(context)
    {
        this.stateName = "Attack";

        this.weapon = weapon;
        fireRate = weapon.GetFireRate();
        target = Vector3.zero;
        this.detector = detector;
        turretTransform = this.weapon.turret;
        cannonTransform = this.weapon.cannon;
    }

    public override void Init()
    {
        
    }
    public override void Update()
    {
        
        target = detector.GetPlayerPosition();

        RotateTurret();

        if (Time.time-lastFireTime > fireRate)
        {
            weapon.Fire(target);
            lastFireTime = Time.time;
        }

        if(!detector.CanSeePlayer())
        {
            context.GetComponent<NPC_Base>().ChangeState("Pursue");
        }
       
    }

    void RotateTurret()
    {
        Vector3 direction = target - turretTransform.position;
        direction.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        turretTransform.rotation = lookRotation;

        cannonTransform.LookAt(target);
    }

    public override void Exit()
    {
        
    }

}
