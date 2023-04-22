using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GS_Helicopter;


//This is for the UI screen that will show the player the mission details. Actual loading of the level will be handled in the game manager.
public class MissionInfoScreen : MonoBehaviour
{
    [SerializeField] Text missionTitleField;
    [SerializeField] Text descriptionField;
    [SerializeField] List<Text> ObjectivesList;

    Mission currentMission;

    //Is currently called from a button press from the previous screen.
    public void SetCurrentMission(string missionID)
    {
        Debug.Log("Setting up mission: " + missionID);
        currentMission = MissionDB.singleton.FetchMission(missionID);

        SetupUI();
    }

   
    void SetupUI()
    {

        if(currentMission==null)
        {
            Debug.Log("No mission set. Cannot populate mission information.");
            return;
        }

        SetMissionNameField();
        PopulateDescriptionField();
        ListOutObjectives();
    }

    void SetMissionNameField()
    {
        missionTitleField.text = currentMission.missionName;
    }

    void PopulateDescriptionField()
    {
        descriptionField.text = currentMission.description;
    }

    public void StartGameButton()
    {
        if(currentMission==null)
        {
            Debug.Log("Cannot load level. No mission is set.");
            return;
        }

        GameManager.singleton.LaunchMission(currentMission.sceneName);
    }

    void ListOutObjectives()
    {
        /*
        for(int i=0; i<currentMission.objectives.Count; i++)
        {
            ObjectivesList[i].gameObject.SetActive(true);
            //ObjectivesList[i].text = currentMission.objectives[i].description;
        }*/
    }

    
}
