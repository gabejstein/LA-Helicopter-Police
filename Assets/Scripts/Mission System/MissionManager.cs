using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public Mission currentMission;

    private void Start()
    {
        if (currentMission != null)
            currentMission.Init();
    }
}
