using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class BisonControl : MonoBehaviour
{
    public float moveSpeed = 0.125f;
    public float distance = 5f;
    public float speedMin, speedMax, speed, timeMin, timeMax, speedConst;
    float timer = 0;
    float RandomizedDelay = 0;
    float spawnTimer = 0;
    public float spawnSpeed;
    bool isFacingRight = false;
    public bool isSpawning = true;
    public bool isFinishing = false;
    public bool isRunning = false;
    public float distanceToFlee;

    private Transform playerTransform;

    Rigidbody2D rb;
    Transform trans;
    // Use this for initialization
    void Start()
    {
        isSpawning = true;
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        spawnTimer += Time.deltaTime;
        if (spawnTimer > 4)
        {
            isSpawning = false;
        }

        float distance = Vector3.Distance(playerTransform.position, trans.position);
        if (distance < distanceToFlee && !isSpawning && !isFinishing)
        {
            isRunning = true;
        } else
        {
            isRunning = false;
        }



        if (isSpawning)
        {
            GetComponent<Collider2D>().enabled = false;
            rb.velocity = Vector2.down * spawnSpeed;
        }

        if(isFinishing)
        {
            rb.velocity = Vector2.down * 5;
        }

        if (!isRunning && !isSpawning && !isFinishing)
        {
            timer += Time.deltaTime;
            if (timer > RandomizedDelay)
            {
                Move();
                RandomizedDelay = Random.Range(timeMin, timeMax);
                timer = 0;
            }
        }
        else if(isRunning)
        {
            //run away from player
            Flee();
        }


    } 

   
    private void Move()
    {
        GetComponent<Collider2D>().enabled = true;


        Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        dir = Vector3.Normalize(dir);

        if (dir.x > 0 && isFacingRight==false)
        {
            Flip();
        }

        if (dir.x < 0 && isFacingRight == true)
        {
            Flip();
        }

        //float speed = Random.Range(speedMin, speedMax);

        Vector2 vel = dir * Random.Range(speedMin, speedMax);

        rb.velocity = vel;
    }

    private void Flee()
    {
        Vector3 run = transform.position - playerTransform.position;
        run.Normalize();
        float runSpeed = speedConst / (Vector3.Distance(playerTransform.position, trans.position));
        rb.velocity = run * runSpeed;
    }



    protected void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Barn" )
        {
              isFinishing = true;
            trans.position = collision.gameObject.transform.position;
        }
        
    }
}
