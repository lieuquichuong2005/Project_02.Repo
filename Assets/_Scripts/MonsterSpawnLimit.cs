using UnityEngine;
using System.Collections;

public class MonsterSpawnLimit : MonoBehaviour
{
    public float minX = 0f;
    public float minY = 0f;
    public float maxX = 0f;
    public float maxY = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLimits(float minX, float minY, float maxX, float maxY)
    {
        this.minX = minX;
        this.minY = minY;
        this.maxX = maxX;
        this.maxY = maxY;
    }

    public float[] GetLimits()
    {
        float[] limits = new float[4] { this.minX, this.minY, this.maxX, this.maxY };
        return limits;
    }
}
