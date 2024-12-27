using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class Spawner_Boss : MonoBehaviour
{
    public GameObject MonsterSpawner;
    public GameObject Pool;

    public int max_spawn;
    bool doneGenerated = false;

    public IEnumerator Start()
    {
        while (!Pool.gameObject.GetComponent<Pool_Boss>().isDoneGeneratePool())
            yield return null;

        max_spawn = Pool.gameObject.GetComponent<Pool_Boss>().getAmountPool();
        for (int i = 1; i <= max_spawn; i++)
        {
            Spawn();
        }
        doneGenerated = true;
    }

    void Update()
    {

        int current_num = Pool.gameObject.GetComponent<Pool_Boss>().returnCount();
        if (current_num == max_spawn)
        {
            MonsterSpawner.gameObject.GetComponent<Timer_Boss>().timerPause();
        }
        else
        {
            MonsterSpawner.gameObject.GetComponent<Timer_Boss>().timerContinue();
        }
    }

    public void Spawn()
    {
        float minX = -31f;
        float minY = -13f;
        float maxX = 31f;
        float maxY = 14f;

        GameObject enemy = Pool.gameObject.GetComponent<Pool_Boss>().GetPooledObject();
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
