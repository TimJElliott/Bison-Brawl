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
	public SpriteRenderer shadowHRenderer;
	public SpriteRenderer shadowVRenderer;
    public float distanceToFlee;

    private Transform playerTransform;
	private Animator anim;

    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        isSpawning = true;
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponentInChildren<Animator> ();
		playerTransform = FindObjectOfType<PlayerControl> ().transform;
    }

    // Update is called once per frame
    void Update()
    {

        spawnTimer += Time.deltaTime;
        if (spawnTimer > 4)
        {
            isSpawning = false;
        }

		float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance < distanceToFlee && !isSpawning && !isFinishing)
        {
            isRunning = true;
        } else
        {
            isRunning = false;
        }



        if (isSpawning)
        {
            //GetComponent<Collider2D>().enabled = false;
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

		if (rb.velocity.y > Mathf.Abs (rb.velocity.x))
			anim.SetBool ("Up", true);
		else
			anim.SetBool ("Up", false);

		if (rb.velocity.y < -Mathf.Abs (rb.velocity.x))
			anim.SetBool ("Down", true);
		else
			anim.SetBool ("Down", false);

		if (rb.velocity.y > Mathf.Abs (rb.velocity.x) || rb.velocity.y < -Mathf.Abs (rb.velocity.x)){
			shadowHRenderer.enabled = false;
			shadowVRenderer.enabled = true;
		} else {
			shadowHRenderer.enabled = true;
			shadowVRenderer.enabled = false;
		}

    } 

   
    private void Move()
    {
		/*
		 * This is not optimal
		 * as if you ever want to
		 * disable the collider,
		 * this will re-enable it
		 * regardless of any outside
		 * actions.
		 * 
		 * I suggest you use Physics2D.IgnoreCollision (coll A, coll B, bool ignore);
		 */

        //GetComponent<Collider2D>().enabled = true;

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
		float runSpeed = speedConst / (Vector3.Distance(playerTransform.position, transform.position));
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

			/*
			 * This will make the bison "teleport" to the barn, which won't look good.
			 * I suggest you create a SetDestination method that takes a vector3 and makes
			 * a boolean true until the bison reaches the destination.
			*/

			transform.position = collision.gameObject.transform.position;
       
		}
        
    }

}
