using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {


	public Image healtBar;
	private float max_health = 100f;
	[SerializeField]
	public float current_health = 0f;
	private PlayerController2D ScriptPlayer;
	// Use this for initialization
	void Start () {
		current_health = max_health;
		SetHealthBar();
		ScriptPlayer = GetComponent<PlayerController2D>();
	}
	
	void Update ()
	{
		if (current_health <= 0)
		{
			ScriptPlayer.isDead = true;
		}
	}
	public void TakeDamage (float amount)
	{
		current_health -= amount;
		SetHealthBar();
	}
	public void SetHealthBar()
	{
		float Player_health = current_health / max_health;
		healtBar.transform.localScale = new Vector3(Mathf.Clamp(Player_health, 0f, 1f), healtBar.transform.localScale.y, healtBar.transform.localScale.z);
	}
}
