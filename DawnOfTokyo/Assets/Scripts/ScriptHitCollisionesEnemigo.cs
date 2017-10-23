using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptHitCollisionesEnemigo : MonoBehaviour {


	PlayerHealth ScriptPlayer;


	// Use this for initialization
	void Start () {
		ScriptPlayer = GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			//ScriptPlayer.current_health = ScriptPlayer.current_health - damageEnemigo;
		}
	}
}
