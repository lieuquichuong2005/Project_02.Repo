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

    float random_timer = 3f;
    int random_move = 0;

    //public float x = 0f;
    //public float y = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        random_move = UnityEngine.Random.Range(1, 5);
        //animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!this.gameObject.GetComponent<MonsterInteract>().AIStatus())
        {
            switch (random_move)
            {
                case 1:
                    this.transform.parent.gameObject.transform.Translate(Vector3.up * Time.deltaTime, Space.World);
                    animator.SetFloat("xVelocity", 0f);
                    animator.SetFloat("yVelocity", 0.2f);
                    break;
                case 2:
                    this.transform.parent.gameObject.transform.Translate(Vector3.down * Time.deltaTime, Space.World);
                    animator.SetFloat("xVelocity", 0f);
                    animator.SetFloat("yVelocity", -0.2f);
                    break;
                case 3:
                    this.transform.parent.gameObject.transform.Translate(Vector3.left * Time.deltaTime, Space.World);
                    animator.SetFloat("xVelocity", -0.2f);
                    animator.SetFloat("yVelocity", 0f);
                    break;
                case 4:
                    this.transform.parent.gameObject.transform.Translate(Vector3.right * Time.deltaTime, Space.World);
                    animator.SetFloat("xVelocity", 0.2f);
                    animator.SetFloat("yVelocity", 0f);
                    break;
            }

            float[] limits = this.transform.parent.gameObject.GetComponent<MonsterSpawnLimit>().GetLimits();
            Transform t = this.transform.parent.gameObject.transform;

            if (t.position.x <= limits[0])
            {
                t.position = new Vector3(limits[0], t.position.y, 0f);
            }
            if (t.position.y <= limits[1])
            {
                t.position = new Vector3(t.position.x, limits[1], 0f);
            }
            if (t.position.x >= limits[2])
            {
                t.position = new Vector3(limits[2], t.position.y, 0f);
            }
            if (t.position.y >= limits[3])
            {
                t.position = new Vector3(t.position.x, limits[3], 0f);
            }

            random_timer -= Time.deltaTime;
            if (random_timer <= 0f)
            {
                random_move = UnityEngine.Random.Range(1, 5);
                random_timer = 3f;
            }
        }
        else
        {
            animator.SetFloat("xVelocity", aiPath.desiredVelocity.x);
            animator.SetFloat("yVelocity", aiPath.desiredVelocity.y);
        } 
    }
}
