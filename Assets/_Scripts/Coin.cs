using UnityEngine;
using System;
using System.Collections;

public class Coin : MonoBehaviour
{
    public int coin_value = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Debug.Log("Coin hit");
            collider.gameObject.GetComponent<PlayerStats>().GainCoin(coin_value);
            Destroy(this.gameObject);
        }
    }
}
