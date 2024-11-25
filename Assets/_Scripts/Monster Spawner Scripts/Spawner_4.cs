using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class Spawner_4 : MonoBehaviour
{
    public GameObject MonsterSpawner;
    public GameObject Pool;

    public int max_spawn;
    int current_num = 0;

    bool doneGenerated = false;

    public IEnumerator Start()
    {
        while (!Pool.gameObject.GetComponent<Pool_4>().isDoneGeneratePool())
            yield return null;

        max_spawn = Pool.gameObject.GetComponent<Pool_4>().getAmountPool();
        for (int i = 1; i <= max_spawn; i++)
        {
            Spawn();
        }
        doneGenerated = true;
    }

    void Update()
    {
        int current_num = Pool.gameObject.GetComponent<Pool_4>().returnCount();
        if (current_num == max_spawn)
        {
            MonsterSpawner.gameObject.GetComponent<Timer_4>().timerPause();
        }
        else
        {
            MonsterSpawner.gameObject.GetComponent<Timer_4>().timerContinue();
        }
    }

    public void Spawn()
    {
        float minX = 7f;
        float minY = -2f;
        float maxX = 14f;
        float maxY = 4f;

        GameObject enemy = Pool.gameObject.GetComponent<Pool_4>().GetPooledObject();
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
}
