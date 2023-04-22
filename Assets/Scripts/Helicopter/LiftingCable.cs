using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS_Helicopter
{
    public class LiftingCable : MonoBehaviour
    {
        private enum LIFT_STATE
        {
            DEFAULT,
            FISHING,
            HANGINGDOWN,
            LIFTING_UP
        }

        [SerializeField] Transform cableOrigin;
        [SerializeField] Transform hookTransform;
        [SerializeField] Hook hook;

        [SerializeField] float cableSpeed = 3f;

        [SerializeField] AudioClip hookSound;

        [SerializeField] LineRenderer lineRenderer;

        AudioSource aSource;

        float cableLength = 8f;
        float cableWidth = 0.1f;

        Transform load;

        LIFT_STATE hookState;


        private void Start()
        {

            aSource = GetComponent<AudioSource>();

            hookState = LIFT_STATE.DEFAULT;

            lineRenderer.positionCount = 2;
            lineRenderer.SetWidth(cableWidth, cableWidth);

        }


        public void UpdateLiftCable()
        {


            switch (hookState)
            {
                case LIFT_STATE.DEFAULT:
                    CheckForTarget();
                    break;
                case LIFT_STATE.FISHING:
                    GoFishing();
                    DrawLine();
                    break;
                case LIFT_STATE.HANGINGDOWN:
                    HangDown();
                    DrawLine();
                    break;
                case LIFT_STATE.LIFTING_UP:
                    LureBackUp();
                    DrawLine();
                    break;
            }

            

        }


        private bool CheckForTarget()
        {
            //TODO: Raycast for pickup objects below helicopter.
            RaycastHit hit;

            if (Physics.Raycast(cableOrigin.position, Vector3.down, out hit))
            {
                if(hit.collider.gameObject.tag=="PickupZone")
                {
                    hookState = LIFT_STATE.FISHING;
                    load = hit.collider.transform;
                    return true;
                }

            }

            return false;
        }

        float t = 0f;
        private void GoFishing()
        {
            if(t<1f)
            {
                float distanceFromLoad = cableOrigin.position.y - load.position.y;
                Vector3 targetPosition = new Vector3(cableOrigin.position.x, cableOrigin.position.y - distanceFromLoad, cableOrigin.position.z);

                t += Time.deltaTime;
                hookTransform.position = Vector3.Lerp(hookTransform.position, targetPosition, t * 0.1f);
         
            }
            else
            {
                hookState = LIFT_STATE.HANGINGDOWN;
                t = 0f;
            }
           
        }

        private void HangDown()
        {
            //If the helicopter flies outside of the pickup zone, lift the hook back up.
            if (CheckForTarget() == false || hook.HasLoad()==true)
            {
                t = 0f;
                hookState = LIFT_STATE.LIFTING_UP;
            }

        }

        private void LureBackUp()
        {
            
            if (t < 1f)
            {
                t += Time.deltaTime;
                hookTransform.position = Vector3.Lerp(hookTransform.position, cableOrigin.position, t * 0.1f);
            }
            else
            {
                hookState = LIFT_STATE.DEFAULT;
                hookTransform.position = cableOrigin.position; //Needs to be set back exactly.
                load = null;
                t = 0f;
            }
            
        }

        void DrawLine()
        {
            if (lineRenderer == null) return;

            lineRenderer.SetPosition(0, cableOrigin.position);
            lineRenderer.SetPosition(1, hookTransform.position);
        }

        
    }
}
