using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class NPC_Base : MonoBehaviour, IDamageable
{
    public Action OnDestroyed;
    public static Action OnDestroyedEnemy;

    [Header("General Info")]
    [SerializeField] protected string npcID;
    [SerializeField] protected int health = 100;
    [SerializeField] protected bool isInvincible = false;

    [Header("AI")]
    protected StateMachine stateMachine;
    protected Dictionary<string, GameState> gameStates = new Dictionary<string, GameState>();

    protected virtual void Start()
    {
        stateMachine = new StateMachine();
    }
    protected virtual void Update()
    {
        if(stateMachine != null)
            stateMachine.Update();
    }

    public void ChangeState(string stateName)
    {
        if (gameStates.ContainsKey(stateName))
        {
            stateMachine.ChangeState(gameStates[stateName]);
        }
    }

    public virtual void GetHit(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    protected virtual void Die()
    {
        if (isInvincible) return;

        OnDestroyed?.Invoke();

        OnDestroyedEnemy?.Invoke();

        GameObject explosion = ObjectPool.singleton.GetObject("Explosion");
        explosion.transform.position = transform.position;

        gameObject.SetActive(false);
        //TO DO: Replace with dead model and make a jump up animation.
    }
}
