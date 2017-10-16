using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	public float maxSpeed = 4f;
	public float jumpForce = 400f;
	private float currentSpeed;
	private Rigidbody body;
	private Animator anim;
	private Transform groundCheck;
	private bool onGround;
	private bool isDead = false;
	private bool facingRight = true;
	private bool jump = false;

	private bool _jumped = false;
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		anim = GetComponent <Animator>();
		groundCheck = gameObject.transform.Find ("GroundCheck");
		currentSpeed = maxSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
		onGround = Physics.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		
		if (Input.GetButtonDown ("Jump") && onGround)
		{
			jump = true;
		}
	}

	private void FixedUpdate()
	{
		if(!isDead)
		{
			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");
			//error
			if (!onGround)
			{
				v = 0;
			}
			//
			if (h != 0 || v != 0)
			{
			body.velocity = new Vector3 (h * currentSpeed, body.velocity.y, v * currentSpeed);
			}
			if (h > 0 && !facingRight)
			{
				Flip();
			}
			else if (h < 0 && facingRight)
			{
				Flip();
			}

			if (jump)
			{
				jump = false;
				body.velocity = new Vector3 (0, jumpForce, 0);
			}

		}
	}
	void Flip()
	{
		facingRight = !facingRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
