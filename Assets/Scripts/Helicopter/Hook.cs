using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    bool hasLoad = false;

    GameObject load = null;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if(hasLoad)
        {
            load.transform.position = transform.position;
            load.transform.rotation = transform.rotation;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Pickup" && load==null)
        {
            load = other.gameObject;
            hasLoad = true;
            load.GetComponent<Pickup>().OnPickedup += ProcessLoad;
        }
    }

    void ProcessLoad()
    {
        load.GetComponent<Pickup>().OnPickedup -= ProcessLoad;
        RemoveLoad();
        
    }

    public bool HasLoad() { return hasLoad; }

    public void RemoveLoad() { 
        hasLoad = false;
        load = null;
    }
}
