using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(monsterPrefab, new Vector3(5f, -7.5f, 0f), Quaternion.identity);
    }

}
