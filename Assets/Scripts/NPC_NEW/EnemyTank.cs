using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : NPC_Base
{
    AILandMover landMover;
    EnemyDetector detector;

    DamageFlash damageFlash;

    [SerializeField] TankCannon cannon;
    //TODO: Make object for tank turret.

    //TODO: Get waypoint path.
    [SerializeField] Transform[] waypointPath;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        landMover = GetComponent<AILandMover>();
        damageFlash = GetComponent<DamageFlash>();
        detector = GetComponent<EnemyDetector>();

        //Initialize states and state machine.
        GameState attackState = new EnemyAttackState(gameObject,cannon,detector);
        GameState idleState = new EnemyIdleState(gameObject,detector);
        GameState patrolState = new EnemyPatrolState(gameObject,waypointPath);
        GameState pursueState = new EnemyPursueState(gameObject,5f);

        gameStates[attackState.GetStateName()] = attackState;
        gameStates[idleState.GetStateName()] = idleState;
        gameStates[patrolState.GetStateName()] = patrolState;
        gameStates[pursueState.GetStateName()] = pursueState;

        stateMachine.StartMachine(patrolState);
    }

    

    // Update is called once per frame
    void Update()
    {
        base.Update();

    }

    public override void GetHit(int damage)
    {
        if (damageFlash != null)
            damageFlash.StartFlash();

        base.GetHit(damage);
    }

    protected override void Die()
    {
        damageFlash.StopFlash();
        base.Die();
    }
}
