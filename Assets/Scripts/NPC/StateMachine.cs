using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GS_Helicopter
{
    public enum FSM_STATE
    {
        IDLE,
        PATROL,
        ATTACK,
        PURSUE,
        ESCAPE,
        DEAD
    }

    public class StateMachine : MonoBehaviour
    {
        [SerializeField] bool GizmosOn = false;

        protected NPC npc;
        protected GameObject player; 
        protected bool hasInitState = false; //a flag so that state initialization code can be called when entering a state. Should be reset upon exiting a state.
        protected Animator anim;
        protected AIMover mover;

        [SerializeField] protected bool friendly;

        [Header("AI Info")]
        [SerializeField] protected FSM_STATE currentState;
        protected FSM_STATE previousState;
        [SerializeField] protected Transform eyePosition;
        [SerializeField] protected float ViewRange = 20f; //How close the player has to be to alert the enemy.
        [SerializeField] protected float ViewAngle = 90f;
        [SerializeField] protected float PursueRange = 15f; //How far the enemy will chase the player. Should be much larger than view range.
        [SerializeField] protected float AttackRange = 15f;
        [SerializeField] protected float TimeToQuitAttack = 10f; //How long the player has to be out of sight before the enemy resumes its patrol.
        
        public GameObject[] waypoints;
        int currentWaypoint = 0;
        [SerializeField] float stoppingDistance = 2f; 

        [SerializeField] protected float restBetweenShots = 1.2f;
        protected float lastTimeFired = 0f;
        protected float lastTimePlayerWasSeen = 0f;

        // Start is called before the first frame update
        void Start()
        {
            npc = GetComponent<NPC>();
            anim = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player");
            mover = GetComponent<AIMover>();

            if (eyePosition == null) eyePosition = transform;
        }

        //---AI Helper Functions---
        protected float DistanceFromPlayer()
        {
            //return Vector3.Distance(player.transform.position, transform.position);
            return (player.transform.position - transform.position).magnitude;
        }

        protected bool CanSeePlayer()
        {
            Vector3 playerDirection = player.transform.position - eyePosition.position;
            float angle = Vector3.Angle(playerDirection, transform.forward);


            if (DistanceFromPlayer() < ViewRange && angle < ViewAngle)
            {
                RaycastHit hit;
                if (Physics.Raycast(eyePosition.position, playerDirection, out hit))
                {

                    if (hit.transform.tag == "Player")
                    {
                        Debug.DrawRay(eyePosition.position, playerDirection, Color.red);
                        lastTimePlayerWasSeen = Time.time;
                        return true;
                    }

                }

            }
            return false;
        }

        protected Vector3 PlayerGroundPosition() //Gets the position directly beneath the player, so ground enemies can move to it.
        {
            RaycastHit hit;

            //note that Vector3.up has to be used instead of transform.up because otherwise the local rotation will affect the raycast.
            if (Physics.Raycast(player.transform.position - new Vector3(0, 3f, 0), -Vector3.up, out hit))
            {
                return hit.point;
            }

            return player.transform.position;
        }

        public virtual void UpdateFSM()
        {
            switch (currentState)
            {
                case FSM_STATE.IDLE:
                    Idle();
                    break;
                case FSM_STATE.PATROL:
                    Patrol();
                    break;
                case FSM_STATE.ATTACK:
                    Attack();
                    break;
                case FSM_STATE.PURSUE:
                    Pursue();
                    break;
                case FSM_STATE.ESCAPE:
                    //Escape
                    break;
                case FSM_STATE.DEAD:

                    break;
            }
        }

        protected void ChangeStates(FSM_STATE newState)
        {
            previousState = currentState;
            currentState = newState;
            hasInitState = false;
        }

        protected virtual void Idle()
        {
            if (CanSeePlayer())
            {
                ChangeStates(FSM_STATE.ATTACK);
            }
        }

        protected virtual void Patrol()
        {
            if (!hasInitState)
            {
                hasInitState = true;
                if (waypoints.Length!=0)
                {
                    mover.SetDestination(waypoints[currentWaypoint].transform.position);
                }     
                else
                {
                    ChangeStates(FSM_STATE.IDLE);
                    return;
                }
                    
            }

            if (waypoints == null)
            {
                //TO DO: Find random position and go there
            }
            else
            {
                Vector3 waypointPos = waypoints[currentWaypoint].transform.position;
                float distance = Vector3.Distance(waypointPos, transform.position);
                if (distance < stoppingDistance)
                {
                    currentWaypoint++;
                    if (currentWaypoint >= waypoints.Length)
                        currentWaypoint = 0;

                    mover.SetDestination(waypoints[currentWaypoint].transform.position);
                }

                if (CanSeePlayer())
                {
                    ChangeStates(FSM_STATE.PURSUE);
                }
            }
        }

        protected virtual void Pursue()
        {
            
            if (DistanceFromPlayer() <= AttackRange && CanSeePlayer())
            {
                ChangeStates(FSM_STATE.ATTACK);
            }
            else
            {
                if (Time.time - lastTimePlayerWasSeen >= TimeToQuitAttack)
                {
                    ChangeStates(FSM_STATE.PATROL);
                    Debug.Log("I give up!");
                }

            }

        }

        protected virtual void Attack()
        {
           
            if (Time.time - lastTimeFired >= restBetweenShots)
            {
                npc.FireShot();
                lastTimeFired = Time.time;
            }

            if (DistanceFromPlayer() >= AttackRange || !CanSeePlayer())
            {
                ChangeStates(FSM_STATE.PURSUE);
            }

        }

        //---End of Finite States---

        private void OnDrawGizmos()
        {
            if (!GizmosOn) return;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, ViewRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRange);

            if (player != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(PlayerGroundPosition(), 0.3f);
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(player.transform.position, 0.3f);
            }

        }
    }
}