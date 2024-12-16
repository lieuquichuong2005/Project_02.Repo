using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeadAnim : MonoBehaviour
{
    public GameObject deadAnimObj;
    public Animator deadAnim;
    bool isDone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDone = false;
        deadAnimObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetStatus()
    {
        return isDone;
    }

    public void UpdateStatus()
    {
        isDone = true;
    }

    public void TriggerAnim()
    {
        deadAnimObj.SetActive(true);
        deadAnim.SetTrigger("Dead");
    }
}
