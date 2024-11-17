using UnityEngine;
using System.Collections.Generic;

public class Pool_1 : MonoBehaviour
{
    public static Pool_1 SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public int returnCount()
    {
        int count = 0;
        for (int i = 0; i < amountToPool; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
            {
                count++;
            }
        }
        return count;
    }

    public int getAmountPool()
    {
        return amountToPool;
    }
}
