using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class MonsterMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public Animator animator;
    public AIPath aiPath;

    bool isUp = false;
    bool isLeft = false;
    bool isRight = false;
    bool isDown = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        /*if (aiPath.desiredVelocity.x >= 0.01f) // go right
        {
            isRight = true;
            isLeft = false;
        }
        else if (aiPath.desiredVelocity.x <= -0.01f) // go left
        {
            isRight = false;
            isLeft = true;
        }

        if (aiPath.desiredVelocity.y >= 0.01f) // go up
        {
            isUp = true;
            isDown = false;
        }
        else if (aiPath.desiredVelocity.y <= -0.01f) // go down
        {
            isUp = false;
            isDown = true;
        }*/

        /*animator.SetBool("isLeft", isLeft);
        animator.SetBool("isRight", isRight);
        animator.SetBool("isUp", isUp);
        animator.SetBool("isDown", isDown);*/

        animator.SetFloat("xVelocity", aiPath.desiredVelocity.x);
        animator.SetFloat("yVelocity", aiPath.desiredVelocity.y);

        Debug.Log("Velocity");
        Debug.Log(aiPath.desiredVelocity.x);
        Debug.Log(aiPath.desiredVelocity.y);
    }
}
