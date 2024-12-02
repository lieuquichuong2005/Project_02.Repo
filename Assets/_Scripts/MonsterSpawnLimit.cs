using UnityEngine;
using System.Collections;

public class MonsterSpawnLimit : MonoBehaviour
{
    public float minX = 0f;
    public float minY = 0f;
    public float maxX = 0f;
    public float maxY = 0f;
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
