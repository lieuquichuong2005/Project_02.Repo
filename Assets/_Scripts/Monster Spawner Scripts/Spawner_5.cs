using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class Spawner_5 : MonoBehaviour
{
    public GameObject MonsterSpawner;
    public GameObject Pool;

    public int max_spawn;
    int current_num = 0;

    bool doneGenerated = false;

    public IEnumerator Start()
    {
        while (!Pool.gameObject.GetComponent<Pool_5>().isDoneGeneratePool())
            yield return null;

        max_spawn = Pool.gameObject.GetComponent<Pool_5>().getAmountPool();
        for (int i = 1; i <= max_spawn; i++)
        {
            Spawn();
        }
        doneGenerated = true;
    }

    void Update()
    {
        int current_num = Pool.gameObject.GetComponent<Pool_5>().returnCount();
        if (current_num == max_spawn)
        {
            MonsterSpawner.gameObject.GetComponent<Timer_5>().timerPause();
        }
        else
        {
            MonsterSpawner.gameObject.GetComponent<Timer_5>().timerContinue();
        }
    }

    public void Spawn()
    {
        float minX, minY, maxX, maxY;

        int random_spawn_pos = UnityEngine.Random.Range(0, 3);
        if (random_spawn_pos == 0)
        {
            minX = -15f;
            minY = -8.3f;
            maxX = -6f;
            maxY = -6f;
        }
        else
        {
            minX = 4f;
            minY = 4f;
            maxX = 13f;
            maxY = 9f;
        }

        GameObject enemy = Pool.gameObject.GetComponent<Pool_5>().GetPooledObject();
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
