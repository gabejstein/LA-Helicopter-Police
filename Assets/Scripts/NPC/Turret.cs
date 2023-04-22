using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS_Helicopter
{
    public class Turret : StateMachine
    {
        [SerializeField] float spinSpeed = 60f;
        public Transform turret; //for handling the turret rotation
        public Transform gun;

        protected override void Attack()
        {
            RotateTurretHead();
            RotateTurretGun();

            base.Attack();

        }

        protected override void Pursue() // of course turrets can't chase after the player, but they can keep the gun trained on them.
        {

            RotateTurretHead();
            RotateTurretGun();
            
            if (CanSeePlayer() && DistanceFromPlayer() <= AttackRange)
                ChangeStates(FSM_STATE.ATTACK);
            else
            {
                if (Time.time - lastTimePlayerWasSeen >= TimeToQuitAttack)
                {
                    ChangeStates(FSM_STATE.PATROL);
                    Debug.Log("I give up!");
                }

            }
        }

        protected override void Patrol() //essentially just spins the head of the turret in a circle till it finds a target.
        {
            turret.Rotate(Vector3.up * spinSpeed * Time.deltaTime);

            if (CanSeePlayer())
                ChangeStates(FSM_STATE.ATTACK);
        }

        void RotateTurretHead()
        {
            Vector3 direction = player.transform.position - turret.position;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0f,direction.z));
            turret.localRotation = lookRotation;
        }

        void RotateTurretGun()
        {
            Vector3 direction = player.transform.position - gun.position;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(0f,direction.y,direction.z));
            gun.localRotation = lookRotation;
        }
    }

   
}