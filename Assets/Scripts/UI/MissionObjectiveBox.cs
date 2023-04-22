using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionObjectiveBox : MonoBehaviour
{
    [SerializeField] Image emptyCheckBox;
    [SerializeField] Sprite checkBoxImage;
    public string objectiveID = "";
    MissionObjective objective;

    public void SetObjectiveText(MissionObjective _objective)
    {
        this.objective = _objective;
        GetComponentInChildren<Text>().text = this.objective.description;
        this.objective.OnCompleted += SetCheckBox;
    }

    public void SetCheckBox()
    {
        Debug.Log("Setting objective check box");
        emptyCheckBox.sprite = checkBoxImage;
        objective.OnCompleted -= SetCheckBox;
    }
}
