using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName ="Mission/New Mission")]
public class Mission : ScriptableObject
{
    public string missionName; //name is kept separated from id in case it needs to be more descriptive.
    public string id;
    public string sceneName; //So the menu manager knows which unity scene is associated with this mission.
    [TextArea] public string description;
    
    public List<MissionObjective> MissionObjectives = new List<MissionObjective>();

    [NonSerialized] private bool missionAccomplished = false;
    [NonSerialized] private bool missionFailed = false;

    public Action OnCompleted;
    public Action OnFailed;

    public void Init()
    {
        foreach (MissionObjective objective in MissionObjectives)
        {
            objective.Initialize(this);
        }

        Debug.Log("Starting Mission " + missionName);
    }

    public void CheckAllObjectives()
    {
        if (missionFailed) return; //Safeguarding against finishing the mission if failure occurs.

        foreach (MissionObjective objective in MissionObjectives)
        {
            if (objective.IsCompleted() == false)
            {
                return;
            }

            //assuming all goals are completed
            missionAccomplished = true;
            OnCompleted?.Invoke();
            Debug.Log("All objectives completed!");
        }
    }

    public bool IsMissionFailure() { return missionFailed; }
    public bool IsMissionAccomplished() { return missionAccomplished; }
}

[System.Serializable]
//this will be a condition that the player must acheive for a mission to be complete
public class MissionObjective : ScriptableObject
{
    public string id;
    public string description;
    //Note: if you want conditions that fail, it might be better just to set the objective to completed and have an objective that tells the mission system that the mission's failed.
    //And if the mission's failed, set completed to false. You might also want to safeguard against checking for completion again in that case.
    //For example, a mission objective might be: Reduce civilian casulties, and it can be completed from the beginning.
    public bool failsMission = false; 
    public Action OnCompleted;

    [NonSerialized] protected bool completed = false;
    protected Mission mission;

    public bool IsCompleted() { return completed; }

    public void Initialize(Mission _mission)
    {
        this.mission = _mission;
        completed = false;
        SubscribeToEvent();
    }

    protected void CompleteGoal()
    {
        completed = true;
        OnCompleted?.Invoke();
        mission.CheckAllObjectives();
    }

    //This should be different depending on the goal. Can make abstract if you wish.
    public virtual void SubscribeToEvent()
    {

    }
}
