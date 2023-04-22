using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS_Helicopter
{
    public class HelicopterStateMachine : StateMachine
    {
        protected override void Attack()
        {
            if(!hasInitState)
            {
                //mover.StopMovement();
            }
            mover.SetDestination(player.transform.position);
            //transform.LookAt(player.transform.position);

            base.Attack();

        }

        protected override void Pursue()
        {
            mover.SetDestination(player.transform.position);

            base.Pursue();
        }
    }
}