using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The very point of this is just so all its child objects can be persistent through each scene.
public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
