using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is for the npcs to be able to see targets and get their positions, etc.
//TODO: Should be modified to look for things other than the player.

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] bool GizmosOn = false;

    protected GS_Helicopter.NPC npc;
    protected GameObject player;

    [SerializeField] protected Transform eyePosition;
    [SerializeField] protected float ViewRange = 20f; //How close the player has to be to alert the enemy.
    [SerializeField] protected float ViewAngle = 90f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (eyePosition == null) eyePosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected float DistanceFromPlayer()
    {
        //return Vector3.Distance(player.transform.position, transform.position);
        return (player.transform.position - transform.position).magnitude;
    }

    public bool CanSeePlayer()
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
                    return true;
                }

            }

        }
        return false;
    }

    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }

    public Vector3 GetPlayerGroundPosition() //Gets the position directly beneath the player, so ground enemies can move to it.
    {
        RaycastHit hit;

        //note that Vector3.up has to be used instead of transform.up because otherwise the local rotation will affect the raycast.
        if (Physics.Raycast(player.transform.position - new Vector3(0, 3f, 0), -Vector3.up, out hit))
        {
            return hit.point;
        }

        return player.transform.position;
    }

    private void OnDrawGizmos()
    {
        if (!GizmosOn) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ViewRange);

        if (player != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(GetPlayerGroundPosition(), 0.3f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(player.transform.position, 0.3f);
        }

    }
}
