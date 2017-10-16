using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	
	Rigidbody body;
	public float vel;
	
	// Use this for initialization
	void Start () {
	body = GetComponent<Rigidbody>();

	}
    void FixedUpdate () {
		
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		if (h != 0 || v != 0)
		{
			body.velocity = new Vector3 (vel * h, 0, vel * v);
		}


	}
}
