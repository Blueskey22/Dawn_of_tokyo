using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	Rigidbody body;
	public float vel;

	//Animator anim;

	private bool _jumped = false;
   	public float jumpForce = 3f;
	float _distToGround;

	// Use this for initialization
	void Start ()
    {
        body = GetComponent<Rigidbody>();
        _distToGround = GetComponent<Collider>().bounds.extents.y;
		//anim = GetComponent<Animator>();
    }

    // Update is called once per frame

	void Update ()
	{
		//Attack();
	}
    void FixedUpdate () 
	{		
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		if (h != 0 || v != 0)
		{
			body.velocity = new Vector3 (vel * h, 0, vel * v);
		}

		if (isGrounded()) 
		{
            if (Input.GetButton("Jump")) 
			{
            	jump();
            }
        }
	}
	void jump() 
	{
         Debug.Log("jumping");
         if (!_jumped) 
		 {
             _jumped = true;
             Vector3 force = transform.up * jumpForce;
             body.AddRelativeForce(force, ForceMode.Impulse);
         }
         _jumped = false;
     }
	bool isGrounded() 
	{
    	return Physics.Raycast(transform.position, -Vector3.up, _distToGround + .1F);
    }



	/*void Attack ()
	{
		if (Input.GetButtonDown ("Fire1"))
		{
			anim.SetBool ("Atacar", true);
			anim.SetBool ("Idle", false);
		}
		else
		{
			anim.SetBool ("Atacar", false);
			anim.SetBool ("Idle", true);
		}
	}*/

}
