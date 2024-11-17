using UnityEngine;
using System.Collections;

public class Timer_1 : MonoBehaviour
{
    public GameObject MonsterSpawner;
    public float spawnTime = 120.0f; //seconds
    public float currentTime;

    bool isPause = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPause)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0.0f)
            {
                timerEnded();
                currentTime = spawnTime;
            }
        }
    }

    void timerEnded()
    {
        MonsterSpawner.gameObject.GetComponent<Spawner_1>().Spawn();
    }

    public void timerPause()
    {
        isPause = true;
    }

    public void timerContinue()
    {
        isPause = false;
    }
}
