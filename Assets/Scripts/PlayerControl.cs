using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class PlayerControl : MonoBehaviour {

    public float speed = 1.0f;
    public bool isFacingRight = true;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D> ();
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

		/*
		 * Never ever ever ever ever handle player physical movement by modifying transform.position. 
		 * Use somthing like rigidBody.addForce (direction * speed) or rigidBody.velocity = newVelocity;
		 */

		//transform.position += move * speed * Time.deltaTime;

		rb.velocity = move * speed * Time.deltaTime;

        if (move.x > 0 && isFacingRight == false)
        {
            Flip();
        }

        if (move.x < 0 && isFacingRight == true)
        {
            Flip();
        }
    }

    protected void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }
}
