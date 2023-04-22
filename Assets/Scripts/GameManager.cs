using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GS_Helicopter
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager singleton;

        PlayerData currentPlayerData;

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
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void LaunchMission(string levelID)
        {
            SceneManager.LoadScene(levelID);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        void testSave()
        {
            currentPlayerData = new PlayerData();
            currentPlayerData.saveSlot = 5;
            currentPlayerData.currentLevel = 7;
            currentPlayerData.missionsCompleted = new List<MissionCompleteData>();

            MissionCompleteData dummyData = new MissionCompleteData();
            dummyData.wasCompleted = true;
            dummyData.missionID = "Riot";
            dummyData.killCount = 47;
            currentPlayerData.missionsCompleted.Add(dummyData);

            MissionCompleteData dummyData2 = new MissionCompleteData();
            dummyData2.wasCompleted = false;
            dummyData2.missionID = "Boob Fest";
            dummyData2.killCount = 162;
            currentPlayerData.missionsCompleted.Add(dummyData2);

            SaveManager.singleton.SaveGame(currentPlayerData);
        }

        public PlayerData GetPlayerData() { return currentPlayerData; }
    }
}