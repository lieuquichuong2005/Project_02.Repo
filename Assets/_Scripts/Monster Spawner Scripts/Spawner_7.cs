using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class Spawner_7 : MonoBehaviour
{
    public GameObject MonsterSpawner;
    public GameObject Pool;

    public int max_spawn;
    int current_num = 0;

    bool doneGenerated = false;

    public IEnumerator Start()
    {
        while (!Pool.gameObject.GetComponent<Pool_7>().isDoneGeneratePool())
            yield return null;

        max_spawn = Pool.gameObject.GetComponent<Pool_7>().getAmountPool();
        for (int i = 1; i <= max_spawn; i++)
        {
            Spawn();
        }
        doneGenerated = true;
    }

    void Update()
    {
        int current_num = Pool.gameObject.GetComponent<Pool_7>().returnCount();
        if (current_num == max_spawn)
        {
            MonsterSpawner.gameObject.GetComponent<Timer_7>().timerPause();
        }
        else
        {
            MonsterSpawner.gameObject.GetComponent<Timer_7>().timerContinue();
        }
    }

    public void Spawn()
    {
        float minX, minY, maxX, maxY;

        int random_spawn_pos = UnityEngine.Random.Range(0, 3);
        if (random_spawn_pos == 0)
        {
            minX = -14f;
            minY = -6f;
            maxX = -8f;
            maxY = -1f;
        }
        else
        {
            minX = 7f;
            minY = -6f;
            maxX = 14f;
            maxY = 1f;
        }

        GameObject enemy = Pool.gameObject.GetComponent<Pool_7>().GetPooledObject();
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
