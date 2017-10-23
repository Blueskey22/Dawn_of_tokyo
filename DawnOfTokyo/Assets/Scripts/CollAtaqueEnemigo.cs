using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollAtaqueEnemigo : MonoBehaviour {


	private Animator anim;
	Enemy ScriptEnemy;

	// Use this for initialization
	void Start () {
		anim = GetComponentInParent<Animator>();
		ScriptEnemy = GetComponentInParent <Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			print (message: "DentroDeTrigger");
			ScriptEnemy.Ataque();
		}
	}
}
