using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//This class if for visualizing a waypoint path
[ExecuteInEditMode]
public class WayPointPath : MonoBehaviour
{
    [SerializeField] float sphereSize = 0.3f;
    [SerializeField] bool viewAsCyclical = true;

    [SerializeField] bool snapToSurface = false;
    [SerializeField] float heightFromSurface = 0.3f;


    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.DrawSphere(transform.GetChild(i).position,sphereSize);
            if(i!=transform.childCount-1)
            {
                Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
            }
            else if(viewAsCyclical)
            {
                Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(0).position);
            }
  
        }
    }

    private void Update()
    {
        if (!snapToSurface) return;

        for (int i = 0; i < transform.childCount; i++)
        {
            RaycastHit ray;
                
            //Note, I made the base position very high up since raycasts cant be detected from below a terrain's collider.
            if (Physics.Raycast(transform.GetChild(i).position + new Vector3(0f,100f,0f), -Vector3.up, out ray))
            {
                if (ray.collider.tag != "Terrain") continue;
                transform.GetChild(i).position = new Vector3(transform.GetChild(i).position.x, ray.point.y + heightFromSurface, transform.GetChild(i).position.z);
            }
                   
            
        }
    }
}
