using UnityEngine;
using System;

public class InfoBoardManager : MonoBehaviour
{
    public GameObject[] boards;
    int current_active;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < boards.Length; i++)
        {
            boards[i].SetActive(false);
        }

        current_active = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < boards.Length; i++)
        {
            if (this.gameObject.transform.GetChild(i).GetComponent<Board>().GetStatus())
            {
                boards[i].SetActive(true);
                current_active = i;
                break;
            }
        }
        
    }

    public void Cancel()
    {
        this.gameObject.transform.GetChild(current_active).GetComponent<Board>().Deactivate();

        for (int i = 0; i < boards.Length; i++)
        {
            boards[i].SetActive(false);
        }
    }
}
