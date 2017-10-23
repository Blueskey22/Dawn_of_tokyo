using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gancho_bala : MonoBehaviour {


    public float tiempo_destruccion_bala;
    public float Disparo_velocidad;
    private Vector3 Disparo_vec;

    private bool ganchoFacingRight;
    // Use this for initialization
    void Start()
    {
        if (Player_Ranged.facingRight == true)
        {
            Disparo_vec = Vector3.right * Disparo_velocidad;
        }
        else
        {
            Disparo_vec = -Vector3.right * Disparo_velocidad;
            Flip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Disparo_vec);
        Destroy(gameObject, tiempo_destruccion_bala);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag != "Enemigo")
        {
            Destroy(col.gameObject);
        }
    }
    void Flip()
	{
		ganchoFacingRight = !ganchoFacingRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
