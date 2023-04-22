using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS_Helicopter {

    //This class is really meant to keep track of all the enemies in the scene and spawn them if needed.
    //TO DO: Implement some enemy flocking behaviour or calls for backup, etc.

    public class EnemyController : MonoBehaviour
    {
        public static EnemyController instance;

        NPC[] enemies; //might change to list if necessary
        GameObject player;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            enemies = FindObjectsOfType<NPC>();
            player = GameObject.FindGameObjectWithTag("Player");

        }

        public NPC[] GetAllEnemies() { return enemies; }
    }
}
