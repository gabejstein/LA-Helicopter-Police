using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    public string itemName;
    public int amount;
    public GameObject prefab;
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool singleton;
    [SerializeField] List<PoolItem> poolItems = new List<PoolItem>(); //what will actually be set in the editor before runtime.

    Dictionary<string, Queue<GameObject>> pooledItems = new Dictionary<string, Queue<GameObject>>();

    // Start is called before the first frame update
    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GeneratePool();
    }

    void GeneratePool()
    {
        foreach (PoolItem item in poolItems)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < item.amount; i++)
            {
                GameObject newObject = Instantiate(item.prefab, transform);
                newObject.SetActive(false);
                queue.Enqueue(newObject);
            }

            pooledItems[item.itemName] = queue;
        }
    }

    public GameObject GetObject(string objectName)
    {
        if (pooledItems.ContainsKey(objectName))
        {
            Queue<GameObject> queue = pooledItems[objectName];
            GameObject item = queue.Dequeue();
            item.SetActive(true);
            queue.Enqueue(item);
            return item;
        }

        return null;
    }

    //Overload to be able to set a position and rotation.
    public GameObject GetObject(string objectName, Vector3 position, Quaternion rotation)
    {
        GameObject poolObject = GetObject(objectName);
        if (poolObject != null)
        {
            poolObject.transform.position = position;
            poolObject.transform.rotation = rotation;
            return poolObject;
        }

        return null;
    }

}
