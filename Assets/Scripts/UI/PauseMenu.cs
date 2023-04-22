using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Mission Objectives Menu")]
    [SerializeField] GameObject missionObjectivesTab;
    [SerializeField] GameObject objectivesPanel;
    [SerializeField] MissionObjectiveBox objectiveBoxPrefab;
    MissionManager missionManager;
    // Start is called before the first frame update
    void Start()
    {
        InitObjectivesMenu();
    }

    private void InitObjectivesMenu()
    {
        missionManager = FindObjectOfType<MissionManager>();

        foreach (MissionObjective objective in missionManager.currentMission.MissionObjectives)
        {
            MissionObjectiveBox newObjective = Instantiate(objectiveBoxPrefab, objectivesPanel.transform);
            newObjective.SetObjectiveText(objective);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            missionObjectivesTab.SetActive(!missionObjectivesTab.activeInHierarchy);
        }
    }
}
