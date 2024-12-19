using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class Spawner_8 : MonoBehaviour
{
    public GameObject MonsterSpawner;
    public GameObject Pool;

    public int max_spawn;
    int current_num = 0;

    bool doneGenerated = false;

    public IEnumerator Start()
    {
        while (!Pool.gameObject.GetComponent<Pool_8>().isDoneGeneratePool())
            yield return null;

        max_spawn = Pool.gameObject.GetComponent<Pool_8>().getAmountPool();
        for (int i = 1; i <= max_spawn; i++)
        {
            Spawn();
        }
        doneGenerated = true;
    }

    void Update()
    {

        int current_num = Pool.gameObject.GetComponent<Pool_8>().returnCount();
        if (current_num == max_spawn)
        {
            MonsterSpawner.gameObject.GetComponent<Timer_8>().timerPause();
        }
        else
        {
            MonsterSpawner.gameObject.GetComponent<Timer_8>().timerContinue();
        }
    }

    public void Spawn()
    {
        float minX = -1.5f;
        float minY = 0.5f;
        float maxX = 15f;
        float maxY = 9f;

        GameObject enemy = Pool.gameObject.GetComponent<Pool_8>().GetPooledObject();
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
