using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptHitCollisions : MonoBehaviour {


	public Collider2D coll;

	// Use this for initialization
	void Start () {
		coll = GetComponent<PolygonCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   void OnTriggerEnter(Collider coll)
    {
		if (coll.gameObject.tag == "Enemy")
		{
			print ("Enemigo");
		}
    }
}
