using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int saveSlot;
    public int currentLevel;
    public List<MissionCompleteData> missionsCompleted;
}

//This is an object that stores player progress for each mission.
public class MissionCompleteData
{
    public bool wasCompleted;
    public string missionID;
    public int killCount;
}
