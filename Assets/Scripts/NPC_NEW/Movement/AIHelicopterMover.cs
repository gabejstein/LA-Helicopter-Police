using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS_Helicopter
{
    [RequireComponent(typeof(HelicopterController))]
    public class AIHelicopterMover : AIMover
    {
        HelicopterController helicopterFlightSystem;
        InputManagerAI AIInputManager;
        bool isStopped = true;

        private void Start()
        {
            helicopterFlightSystem = GetComponent<HelicopterController>();
            AIInputManager = GetComponent<InputManagerAI>();
        }

        private void Update()
        {
            if (isStopped) return;
            MoveToPoint();
            helicopterFlightSystem.UpdateFlightSystem(AIInputManager);
        }

        public override void SetDestination(Vector3 destination)
        {
            this.destination = destination;
            isStopped = false;
        }

        private void MoveToPoint()
        {
            if (destination == null) return;

            Vector3 distance = (destination - transform.position).normalized;

            AdjustAltitude(distance);
            AdjustYaw(distance);

            AIInputManager.SetVertical(1f); //should replace with some sort of forward speed or acceleration adjusted by the distance.

        }

        private void AdjustYaw(Vector3 distance)
        {
            float destAngle = Vector3.SignedAngle(transform.forward, distance, Vector3.up);

            AIInputManager.SetYaw(destAngle * 0.1f);
        }

        private void AdjustAltitude(Vector3 distance)
        {
            if (Mathf.Sign(distance.y) == 1)
            {
                AIInputManager.SetAscent(Mathf.Abs(distance.y));
                AIInputManager.SetDescent(0f);
            }
            else
            {
                AIInputManager.SetAscent(0f);
                AIInputManager.SetDescent(Mathf.Abs(distance.y));
            }

            //To Do: Make sure helicopter doesn't fly too close to the ground using a raycast.
        }

        public override void StopMovement()
        {
            isStopped = true;
            AIInputManager.SetVertical(0f);
            AIInputManager.SetYaw(0f);
        }
    }
}