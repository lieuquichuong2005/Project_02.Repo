using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class Spawner_1 : MonoBehaviour
{
    public GameObject MonsterSpawner;
    public GameObject Pool;

    /*public GameObject level1Prefab;
    List<GameObject> enemies = new List<GameObject>();*/

    public int max_spawn;
    int current_num = 0;

    bool doneGenerated = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*void Start()
    {
        for (int i = 1; i <= max_spawn; i++)
        {
            GameObject new_spawn = Spawn();
            enemies.Add(new_spawn);
            current_num++;
        }
    }*/

    public IEnumerator Start()
    {
        while (!Pool.gameObject.GetComponent<Pool_1>().isDoneGeneratePool())
            yield return null;

        //max_spawn = Pool_1.SharedInstance.getAmountPool();
        max_spawn = Pool.gameObject.GetComponent<Pool_1>().getAmountPool();
        for (int i = 1; i <= max_spawn; i++)
        {
            Spawn();
        }
        doneGenerated = true;
    }

    // Update is called once per frame
    /*void Update()
    {
        if (current_num == max_spawn)
        {
            MonsterSpawner.gameObject.GetComponent<Timer_1>().timerPause();
        }
        else
        {
            MonsterSpawner.gameObject.GetComponent<Timer_1>().timerContinue();
        }

        if (enemies.Count != 0)
        {
            foreach (GameObject enemy in enemies)
            {
                bool isDead = enemy.gameObject.transform.GetChild(0).GetComponent<MonsterInteract>().Status();
                if (isDead)
                {
                    int index = enemies.IndexOf(enemy);
                    List<GameObject> new_list = new List<GameObject>();
                    new_list = UpdateList(index);
                    enemies = new_list;
                    current_num--;
                    break;
                }
            }
        }
    }*/

    void Update()
    {
        int current_num = Pool.gameObject.GetComponent<Pool_1>().returnCount();
        if (current_num == max_spawn)
        {
            MonsterSpawner.gameObject.GetComponent<Timer_1>().timerPause();
        }
        else
        {
            MonsterSpawner.gameObject.GetComponent<Timer_1>().timerContinue();
        }
    }

    /*public GameObject Spawn()
    {
        float minX = -1.5f;
        float minY = -5f;
        float maxX = 16f;
        float maxY = 10f;

        GameObject new_spawn = Instantiate(level1Prefab, new Vector3(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY), 0f), Quaternion.identity);
        return new_spawn;
    }*/

    public void Spawn()
    {
        float minX = 5f;
        float minY = 1f;
        float maxX = 15f;
        float maxY = 8.5f;

        GameObject enemy = Pool.gameObject.GetComponent<Pool_1>().GetPooledObject();
        if (enemy != null)
        {
            enemy.transform.position = new Vector3(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY), 0f);
            enemy.transform.rotation = Quaternion.identity;
            enemy.gameObject.GetComponent<Seeker>().enabled = false;
            enemy.gameObject.GetComponent<AIPath>().enabled = false;
            enemy.gameObject.GetComponent<AIDestinationSetter>().enabled = false;
            enemy.gameObject.GetComponent<MonsterSpawnLimit>().SetLimits(minX, minY, maxX, maxY);
            enemy.SetActive(true);
            if (doneGenerated)
            {
                enemy.gameObject.transform.GetChild(0).GetComponent<MonsterInteract>().Revive();
            }
        }
    }

    /*public List<GameObject> UpdateList(int index)
    {
        List<GameObject> new_list = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            if (enemies.IndexOf(enemy) != index)
            {
                new_list.Add(enemy);
            }
            else
            {
                GameObject deadObj = enemy;
                Destroy(deadObj);
            }
        }

        return new_list;
    }*/
}
