using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    Stack<GameState> stateStack = new Stack<GameState>();

    GameState currentState;
    GameState persistentState; //Obviously a state that will always run like looking for enemies, etc.

    public StateMachine()
    {

    }

    public void StartMachine(GameState startState, GameState persistentState = null)
    {

        this.currentState = startState;
        this.persistentState = persistentState;

        if(currentState != null)
            currentState.Init();

        if(persistentState != null)
            persistentState.Init();
    }

    // Update is called once per frame
    public void Update()
    {
        if (currentState != null)
            currentState.Update();

        if (persistentState != null)
            persistentState.Update();
    }

    public void ReturnToLastState()
    {
        if(currentState != null && stateStack.Count > 0)
        {
            currentState.Exit();
            stateStack.Pop();

            currentState = stateStack.Peek();
            currentState.Init();
        }
            
    }

    public void ChangeState(GameState newState)
    {
        if (newState == currentState) return;

        if(currentState !=null)
            currentState.Exit();

        currentState = newState;
        currentState.Init();

        stateStack.Push(newState);

    }
}
