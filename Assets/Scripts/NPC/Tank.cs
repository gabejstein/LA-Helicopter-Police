using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS_Helicopter
{
    public class Tank : StateMachine
    {
        public GameObject turret; //for handling the turret rotation
       
        protected override void Attack()
        {
            if (!hasInitState)
            {
                hasInitState = true;
                mover.StopMovement();
            }

            turret.transform.LookAt(player.transform.position);
            base.Attack();

        }

        protected override void Pursue()
        {
            mover.SetDestination(PlayerGroundPosition());

            base.Pursue();
        }
    }
}
