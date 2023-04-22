using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GS_Helicopter
{
    public class HUD : MonoBehaviour
    {
        Player player;
        AmmoHolder ammoHolder;
        public GameObject healthDisplay;
        public GameObject fuelDisplay;
        public GameObject bulletDisplay;
        public GameObject missileDisplay;

        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<Player>();
            ammoHolder = player.GetComponent<AmmoHolder>();
        }

        // Update is called once per frame
        void Update()
        {
            int healthAmount = player.GetHealth();
            int fuelAmount = player.GetFuelAmount();

            healthDisplay.GetComponent<Text>().text = "Health: " + healthAmount.ToString();
            fuelDisplay.GetComponent<Text>().text = "Fuel: " + fuelAmount.ToString();
            bulletDisplay.GetComponent<Text>().text = "Bullets: " + ammoHolder.GetAmount(AMMO_TYPE.GATTLING);
            missileDisplay.GetComponent<Text>().text = "Missiles: " + ammoHolder.GetAmount(AMMO_TYPE.H_MISSILE);
        }
    }
}