using UnityEngine;
using System.Collections.Generic;

public class Pool_8 : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject[] lvl7_Pool = new GameObject[3];
    public GameObject[] lvl8_Pool = new GameObject[4];
    //public GameObject objectToPool;
    public int amountToPool;

    bool doneGeneratePool = false;

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            if (i < amountToPool / 2)
            {
                int random_index = UnityEngine.Random.Range(0, 3);
                tmp = Instantiate(lvl7_Pool[random_index]);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
            else
            {
                int random_index = UnityEngine.Random.Range(0, 4);
                tmp = Instantiate(lvl8_Pool[random_index]);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
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
