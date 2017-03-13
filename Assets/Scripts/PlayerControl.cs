using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class PlayerControl : MonoBehaviour {

	public SpriteRenderer shadowHRenderer;
	public SpriteRenderer shadowVRenderer;
    public float speed = 1.0f;
	public float speedDeadzone;
	public float animSpeedMultiplier;
    public bool isFacingRight = true;

	private Rigidbody2D rb;
	private Animator guyAnim;
	private Animator horseAnim;
	private bool isMoving;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D> ();

		guyAnim = GetComponentsInChildren<Animator> () [0];
		horseAnim = GetComponentsInChildren<Animator> () [1];
		
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


		//Set the horse and player animations depending on whether they're going up or down	
		if (rb.velocity.y < -Mathf.Abs (rb.velocity.x)) {
			shadowHRenderer.enabled = false;
			shadowVRenderer.enabled = true;
			guyAnim.SetBool ("Up", false);
			horseAnim.SetBool ("Up", false);
			guyAnim.SetBool ("Down", true);
			horseAnim.SetBool ("Down", true);
		} else if (rb.velocity.y > Mathf.Abs (rb.velocity.x)) {
			shadowHRenderer.enabled = false;
			shadowVRenderer.enabled = true;
			guyAnim.SetBool ("Up", true);
			horseAnim.SetBool ("Up", true);
			guyAnim.SetBool ("Down", false);
			horseAnim.SetBool ("Down", false);
		} else {
			shadowHRenderer.enabled = true;
			shadowVRenderer.enabled = false;
			guyAnim.SetBool ("Up", false);
			horseAnim.SetBool ("Up", false);
			guyAnim.SetBool ("Down", false);
			horseAnim.SetBool ("Down", false);
		}

		if (Mathf.Abs (rb.velocity.x) > speedDeadzone || Mathf.Abs (rb.velocity.y) > speedDeadzone)
			isMoving = true;
		else 
			isMoving = false;

		guyAnim.SetBool ("Moving", isMoving);
		horseAnim.SetBool ("Moving", isMoving);

		if (isMoving) {
			guyAnim.speed = rb.velocity.magnitude * animSpeedMultiplier;
			horseAnim.speed = rb.velocity.magnitude * animSpeedMultiplier;
		} else {
			guyAnim.speed = 1;
			horseAnim.speed = 1;
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
