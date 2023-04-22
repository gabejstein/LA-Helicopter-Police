using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS_Helicopter
{
    [RequireComponent(typeof(Rigidbody))]
    public class HelicopterController : MonoBehaviour
    {
        private enum HELI_STATE
        {
            AIRBORNE,
            LANDING,
            TAKING_OFF,
            FALLING,
            GROUNDED
        }

        Rigidbody rb;

        float horInput;
        float vertInput;
        float speed = 10f;
        [SerializeField] float yawSpeed = 20f;
        float propellerSpeed = 800f;
        float yawInput;
        Vector3 turnAngles = new Vector3(0, 0, 0);
        float bankingAmount = 25f;
        Quaternion finalRot = Quaternion.identity; //use to update the banking angle

        float setAltitude = 8.05f;

        [SerializeField] GameObject propeller;
        [SerializeField] GameObject backPropeller;
        [SerializeField] GameObject heliBody;

        [Header("Sounds")]
        [SerializeField] AudioClip engineSFX;
        AudioSource audioSource;

        HELI_STATE helicopterState;

        bool outOfFuel = false;

        Transform landPoint;

        Vector3 dampVelocity = Vector3.zero;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();

            audioSource.clip = engineSFX;
            audioSource.Play();

            helicopterState = HELI_STATE.AIRBORNE;
        }

        // Update is called once per frame
        public void UpdateFlightSystem(InputManager inputManager)
        {
            HandleInputs(inputManager);

        }

        private void HandleInputs(InputManager inputManager)
        {
            horInput = inputManager.GetHorizontal();
            vertInput = inputManager.GetVertical();

            yawInput = inputManager.GetYaw();
        }

        private void FixedUpdate()
        {
            

            switch(helicopterState)
            {
                case HELI_STATE.AIRBORNE:
                    HandleFlight();
                    AnimatePropeller();
                    MaintainAltitude();
                    CheckForHelipad();
                    break;
                case HELI_STATE.LANDING:
                    rb.velocity = Vector3.zero;
                    rb.isKinematic = true;
                    rb.angularVelocity = Vector3.zero;
                    rb.freezeRotation = true;
            
                    AnimatePropeller();
                    LandHelicopter();
                    break;
                case HELI_STATE.TAKING_OFF:
                    TakeOff();
                    break;
                case HELI_STATE.FALLING:
                    HandleFlight(); //allow player to steer anyways just so they can control their crash.
                    //TO DO: Decelerate propeller animation
                    //TO DO: Play engine screeching sound effect.
                    //TO DO: Check for collisions and explode.
                    break;
                case HELI_STATE.GROUNDED:
                    AnimatePropeller();
                    IsGrounded();
                    break;
            }

            
        }

        private void HandleFlight()
        {
   
            rb.AddForce(transform.forward * vertInput * speed, ForceMode.Acceleration);
            rb.AddForce(transform.right * horInput * speed, ForceMode.Acceleration);

            Banking();
        }

        private void Banking()
        {
            turnAngles.x = vertInput * bankingAmount;
            turnAngles.z = -horInput * bankingAmount;
            turnAngles.y += yawInput * yawSpeed;
            Quaternion rotAmount = Quaternion.Euler(0, turnAngles.y, 0);
            rb.MoveRotation(rotAmount);

            Quaternion bankingRot = Quaternion.Euler(turnAngles.x, turnAngles.y, turnAngles.z);
            finalRot = Quaternion.Slerp(finalRot, bankingRot, Time.deltaTime * 3f);
            /*heliBody.*/transform.rotation = (finalRot);
        }

        private void AnimatePropeller()
        {
            propeller.transform.Rotate(Vector2.up * propellerSpeed * Time.deltaTime);
            backPropeller.transform.Rotate(Vector2.up * propellerSpeed * Time.deltaTime);
        }

        void MaintainAltitude()
        {
            Vector3 FixedPos = new Vector3(transform.position.x, setAltitude, transform.position.z);

            transform.position = FixedPos;
        }

        public void RunOutOfFuel()
        {
            rb.useGravity = true;
            outOfFuel = true;
            helicopterState = HELI_STATE.FALLING;
        }

        float currentPercent = 0f;
        float currentPercent2 = 0f;
        float posOverPad;
        void LandHelicopter()
        {
            float distFromLandPoint = transform.position.y - landPoint.position.y;
            Vector3 lateralPosition = new Vector3(landPoint.position.x,posOverPad-distFromLandPoint,landPoint.position.z);

            
            if (currentPercent<1f)
            {
                currentPercent += Time.deltaTime * 0.8f;

                transform.position = Vector3.Lerp(transform.position, lateralPosition, currentPercent);
                transform.rotation = Quaternion.Lerp(transform.rotation, landPoint.rotation, currentPercent);
            }
            else if(currentPercent2<1f)
            {
                Debug.Log("Going down.");
                currentPercent2 += Time.deltaTime * 0.8f;
                
                transform.position = Vector3.Lerp(transform.position, landPoint.position, currentPercent2); ;
            }
            else
            {
                Debug.Log("Has landed");
                helicopterState= HELI_STATE.GROUNDED;
                currentPercent = 0;
                currentPercent2 = 0;
            }
            
        }

        void IsGrounded()
        {
            //transform.position = landPoint.position; //need to fix in place.
            //transform.rotation = landPoint.rotation;
            //Delete below later.
            Debug.Log("Is Grounded");
            if(Input.GetKeyDown(KeyCode.Return))
                helicopterState = HELI_STATE.AIRBORNE;
        }

        void TakeOff()
        {
           
        }

        void CheckForHelipad()
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position,Vector3.down, out hit))
            {
                if(hit.collider.gameObject.tag=="Helipad")
                {
                    Debug.Log("Over helipad now");
                    helicopterState = HELI_STATE.LANDING;
                    landPoint = hit.collider.gameObject.transform;
                    posOverPad = transform.position.y;
                }
            }
        }

    }
}
