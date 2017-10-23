using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	public Image healtBar;
	private float max_health = 100f;
	[SerializeField]
	private float current_health = 0f;

	// Use this for initialization
	void Start () {
		current_health = max_health;
		SetHealthBar();
	}
	
	public void TakeDamage (float amount)
	{
		current_health -= amount;
		SetHealthBar();
	}
	public void SetHealthBar()
	{
		float enemy_health = current_health / max_health;
		healtBar.transform.localScale = new Vector3(Mathf.Clamp(enemy_health, 0f, 1f), healtBar.transform.localScale.y, healtBar.transform.localScale.z);
	}
}
