using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS_Helicopter;
using System;

[CreateAssetMenu(menuName ="Mission/Objectives/New DestroyAllEnemies")]
public class DestroyAllEnemiesObjective : MissionObjective
{
    [NonSerialized] int enemiesRemaining = 2; //hardcoding this just for now.
    public override void SubscribeToEvent()
    {
        NPC.OnDestroyedEnemy += CheckCondition;
        enemiesRemaining = FindObjectsOfType<NPC>().Length;
    }

    void CheckCondition()
    {
        enemiesRemaining--;
        Debug.Log("enemies remaining: " + enemiesRemaining);
        if (enemiesRemaining <= 0)
            CompleteGoal();
    }
}
