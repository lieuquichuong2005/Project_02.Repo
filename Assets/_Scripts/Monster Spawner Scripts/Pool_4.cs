using UnityEngine;
using System.Collections.Generic;

public class Pool_4 : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject[] lvl3_Pool = new GameObject[3];
    public int amountToPool;

    bool doneGeneratePool = false;

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            int random_index = UnityEngine.Random.Range(0, 3);
            tmp = Instantiate(lvl3_Pool[random_index]);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }

        doneGeneratePool = true;
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

    public bool isDoneGeneratePool()
    {
        return doneGeneratePool;
    }
}
