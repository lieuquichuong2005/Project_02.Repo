using UnityEngine;
using System.Collections.Generic;

public class Pool_1 : MonoBehaviour
{
    //public static Pool_1 SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject[] lvl1_Pool = new GameObject[4];
    //public GameObject objectToPool;
    public int amountToPool;

    bool doneGeneratePool = false;

    /*void Awake()
    {
        SharedInstance = this;
    }*/

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            int random_index = UnityEngine.Random.Range(0, 4);
            tmp = Instantiate(lvl1_Pool[random_index]);
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
