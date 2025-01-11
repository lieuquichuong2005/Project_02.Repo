using UnityEngine;
using System;
using System.Collections;

public class Coin : MonoBehaviour
{
    public int coin_value = 10;
    public AudioSource coin_sound;
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
            coin_sound.Play();
            StartCoroutine(waitForSound(collider));
            
        }
    }

    IEnumerator waitForSound(Collider2D collider)
    {
        //Wait Until Sound has finished playing
        while (coin_sound.isPlaying)
        {
            yield return null;
        }

        //Auidio has finished playing, disable GameObject
        collider.gameObject.GetComponent<PlayerStats>().GainCoin(coin_value);
        Destroy(this.gameObject);
    }
}
