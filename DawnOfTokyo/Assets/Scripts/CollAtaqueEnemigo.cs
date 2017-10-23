using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollAtaqueEnemigo : MonoBehaviour {


	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponentInParent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.GetComponent<PlayerController2D>())
		{
			print (message: "DentroDeTrigger");
			anim.SetTrigger("Atacar");
		}
	}
}
