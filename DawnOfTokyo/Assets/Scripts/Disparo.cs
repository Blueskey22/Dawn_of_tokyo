using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour {

    public GameObject Disparo_obj;
    public GameObject Disparo_cargado_obj;
    public Transform Disparo_pos;
    public float Disparo_delta = 0.5F;
    public float Siguiente_disparo = 1F;
    public float Mitiempo = 0F;
    public float Tiempo_carga = 3F;

    // Use this for initialization
    // Use this for initialization
    void Start()
    {
     
    }

    // Update is called once per frame
    void FixedUpdate () {
        Mitiempo = Mitiempo + Time.deltaTime;
        if (Input.GetButton("Fire_ranged") && (Mitiempo > Tiempo_carga))
        {
            Siguiente_disparo = Mitiempo + Disparo_delta;
            Instantiate(Disparo_cargado_obj, Disparo_pos.position, Quaternion.identity);
            Siguiente_disparo = Siguiente_disparo - Mitiempo;
            Mitiempo = 0.0F;            
        }
        else if (Input.GetButton("Fire_ranged") && (Mitiempo>Siguiente_disparo) )
        {
            Siguiente_disparo = Mitiempo + Disparo_delta;
            Instantiate(Disparo_obj, Disparo_pos.position, Quaternion.identity);
            Siguiente_disparo = Siguiente_disparo - Mitiempo;
            Mitiempo = 0.0F;
        }
       
    }
}
