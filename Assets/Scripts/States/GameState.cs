using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    protected StateMachine stateMachine;
    protected string stateName;

    protected GameObject context;

    public GameState(GameObject context)
    {
        this.context = context;
    }

    public string GetStateName() { return stateName; }

    public void SetStateMachine(StateMachine _stateMachine)
    {
        this.stateMachine = _stateMachine;
    }

    public abstract void Init();

    public abstract void Update();

    public abstract void Exit();
    
}
