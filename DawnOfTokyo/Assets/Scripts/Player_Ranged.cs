using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ranged : MonoBehaviour {

	public float maxSpeed = 4f;
	public float jumpForce = 5f;
	//public Sprite spriteAtaque;
	public Sprite spriteAndar;
	public SpriteRenderer sr;
	//public Transform  posEscudo;
	//public GameObject shield;
//	float tiempo = 0f;


	private float currentSpeed;
	private Rigidbody body;
	//private Animator anim;
	private bool onGround;
	private bool isDead = false;
	static public bool facingRight = true;
	private bool jump = false;
	//private SphereCollider CollEsfera;


	//private bool IsCourutineExecuting = false;


	//private int hitRange = 10;

	RaycastHit hit;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		//anim = GetComponent <Animator>();
		sr = GetComponent<SpriteRenderer>();
		//CollEsfera = GetComponent<SphereCollider>();
		currentSpeed = maxSpeed;
		//CollEsfera.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		int layerMask = 1 << 8;

		
		if (Physics.Raycast(transform.position, transform.TransformDirection (-Vector3.up), out hit, 5, layerMask)) 
		{
		onGround = false;
        Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.forward) * hit.distance, Color.yellow);
        //Debug.Log("Did Hit");
    	}
		else
		{
			onGround = true;
		}


		float j = Input.GetAxis ("Jump2");
		if (j > 0 && onGround)
		{
			jump = true;
		}
	}

	private void FixedUpdate()
	{
		if(!isDead)
		{
			float h = Input.GetAxis ("Horizontal Range");
			float v = Input.GetAxis ("Vertical Range");
			
			if (!onGround)
			{
				v = 0;
			}
			
			if (h != 0 || v != 0)
			{
			body.velocity = new Vector3 (h * currentSpeed, body.velocity.y, v * currentSpeed);
			sr.sprite = spriteAndar;
			}
			if (h == 0 && v == 0)
			{
				h = 0;
				v = 0;
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
