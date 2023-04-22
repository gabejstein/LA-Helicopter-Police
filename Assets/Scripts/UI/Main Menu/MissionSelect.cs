using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GS_Helicopter;

public class MissionSelect : MonoBehaviour
{
    PlayerData pData;

    int numOfUnlockedMissions = 0;

    [SerializeField] List<GameObject> missionButtons;
    [SerializeField] GameObject selectionPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        pData = GameManager.singleton.GetPlayerData();

        DepopulateMissionButtons();
        PopulateMissionButtons();
    }

    void PopulateMissionButtons()
    {
        for(int i=0;i<numOfUnlockedMissions;i++)
        {
            //TODO: Pull mission from the mission DB;
            Mission mission;
            //TODO: Set up thumbnail.

            //TODO: Set up name text.

            //TODO: Set button up button listener.
        }
    }

    void DepopulateMissionButtons()
    {

    }


}
