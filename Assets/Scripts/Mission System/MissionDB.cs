using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDB : MonoBehaviour
{

    public static MissionDB singleton;

    Dictionary<string, Mission> missionDictionary = new Dictionary<string, Mission>();

    [SerializeField] List<Mission> missionList = new List<Mission>(); //so that they can be placed in the database in the editor.

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    void Start()
    {
        PopulateMissionDictionary();
    }

    void PopulateMissionDictionary()
    {
        foreach (Mission mission in missionList)
        {
            missionDictionary[mission.id] = mission;
        }
    }
    
    public Mission FetchMission(string id)
    {
        if (missionDictionary.ContainsKey(id))
        {
            return missionDictionary[id];
        }
        else
        {
            Debug.Log("Mission was not found in the database.");
            return null;
        }
            
    }
}
