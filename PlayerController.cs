using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	public float maxSpeed = 4f;
	public float jumpForce = 400f;
	public Sprite spriteAtaque;
	public Sprite spriteAndar;
	public SpriteRenderer sr;

	private Sprite ChangeSprite;
	private float currentSpeed;
	private Rigidbody body;
	private Animator anim;
	private bool onGround;
	private bool isDead = false;
	private bool facingRight = true;
	private bool jump = false;

	private int hitRange = 10;

	RaycastHit hit;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		anim = GetComponent <Animator>();
		//ChangeSprite = GetComponent<Sprite>();
		sr = GetComponent<SpriteRenderer>();
		currentSpeed = maxSpeed;
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


		float j = Input.GetAxis ("Jump");
		if (j > 0 && onGround)
		{
			jump = true;
		}

		//ATTACK

		if (Input.GetButtonDown ("Fire1"))
		{
			Debug.Log ("Atacando");
			Attack();
		}
	}

	private void FixedUpdate()
	{
		if(!isDead)
		{
			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");
			
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

	void Attack()
	{
		RaycastHit hit1;
		Vector3 forward =Vector3.up;
		Vector3 origen = transform.position;

		sr.sprite = spriteAtaque;

		if (Physics.Raycast(origen, forward, out hit1, hitRange))
		{
			Debug.Log("primer IF");
			if (transform.gameObject.tag == "Enemy")
         	{
             	transform.gameObject.SendMessage("TakeDamage", 30);
				Debug.Log("Ha dado a enemigo");
         	}
		}	
		Debug.Log ("ya");
		
	}
}
