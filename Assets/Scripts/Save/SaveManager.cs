using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GS_Helicopter
{
    public class SaveManager : MonoBehaviour
    {
        const int MAX_SAVE_SLOTS = 4;

        PlayerData[] playersData;

        public static SaveManager singleton;

        string fileExtension = ".sav";
        string path;

        private void Awake()
        {
            if(singleton==null)
            {
                singleton = this;
            }
            else
            {
                DestroyImmediate(this);
            }
        }

        private void Start()
        {

            playersData = new PlayerData[MAX_SAVE_SLOTS];

            LoadAllSaves();
        }

        //This is mainly for the UI system.
        public PlayerData[] GetPlayersData()
        {
            return playersData;
        }

        public void SaveGame(PlayerData playerData)
        {
            string fileName = "player" + "00" + playerData.saveSlot.ToString() + fileExtension;
            string fullPath = Application.persistentDataPath + "\\" + fileName;
            string data = "";

            
            //The following is just a simple serializer that converts things to a csv format.
            FileStream fStream = File.OpenWrite(fileName);
            StreamWriter writer = new StreamWriter(fStream);

            
            data += playerData.saveSlot.ToString() + ",";
            data += playerData.currentLevel.ToString() + "\n";

            for(int i=0;i<playerData.missionsCompleted.Count;i++)
            {
                MissionCompleteData mcd = playerData.missionsCompleted[i];
                data += mcd.missionID + ",";
                data += mcd.wasCompleted.ToString() + ",";
                data += mcd.killCount.ToString();
                data += "\n";
                
            }

            writer.Write(data);
            
            writer.Close();

            //Might also want to update the save data here in case the player decides to go back to the main menu.
            //Although it may not be neessary since everythng's being passed along as a reference.
        }

        private void LoadAllSaves()
        {
            //TODO: Check folder for save files and load them into the playersData array.
        }

        private void LoadSaveFile(int fileSlot)
        {
            string fileName = "player" + "00" + fileSlot.ToString() + fileExtension;
            string data = "";
            StreamReader reader = new StreamReader(fileName);

            data = reader.ReadToEnd();

            reader.Close();

            PlayerData pData = new PlayerData();

            //TODO: Split data into separate lines
            //TODO: Split first line by commas and place respective members into object.
            //TODO: Loop through remainder of lines and comma split those up.

            playersData[fileSlot] = pData;
        }
    }
}