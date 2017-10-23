using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{

    float health = 100f;

    public float maxSpeed = 4f;
    public float jumpForce = 3f;
    public Transform posEscudo;
    public Transform posEscudoRanged;
    public GameObject shield;
//    float tiempo = 0f;


    
    private float currentSpeed;
    private Rigidbody2D body;
    private Animator anim;
    private bool onGround;
    public bool isDead = false;
    private bool facingRight = true;
    private bool jump = false;


    //VariablesAtaque
    //float damage = 5f;
    bool Atacado = false;





    RaycastHit hit;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren <Animator>();
        health = 100f;
        currentSpeed = maxSpeed;
   
    }


    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 8;

        if (health <= 0)
        {
            isDead = true;
        }

        if (Physics2D.Raycast(transform.position, transform.TransformDirection(-Vector2.up) /*out hit*/, 0.1f, layerMask))
        {
            onGround = true;
            anim.ResetTrigger("Jump");
            anim.ResetTrigger("AtaqueCaida");
            anim.SetBool ("Land", false);
            anim.SetTrigger ("Idle");
            anim.SetBool("onGround", true);
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector2.up) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        else
        {
            onGround = false;
            anim.SetBool("onGround", false);
        }

       float j = Input.GetAxis("Jump");

       if (j > 0 && onGround)
        {
            jump = true;
            anim.SetTrigger ("Jump");
        }

        //SHIELD

        if (Input.GetButtonDown("Fire2"))
        {
            Escudo();
        }

     

        //SHIELD COMPAÑERO

        if (Input.GetButtonDown("Jump") && Input.GetButton("Fire2") || Input.GetButton("Jump") && Input.GetButton("Fire2"))
        {
            PonerEscudoAmigo();
            Debug.Log("PonerEscudoAmigo");
        }

        /*//STUN
		if (Input.GetButtonDown ("Fire2") && Input.GetButton ("Fire1") || Input.GetButtonDown ("Fire1") && Input.GetButton ("Fire2"))
		{
			Debug.Log ("Stun");
			tiempo = tiempo + Time.deltaTime;
			//print (tiempo);
			Stun();
			if (CollEsfera.enabled == true || tiempo > 3f)
			{
				CollEsfera.enabled = false;
				Debug.Log("SphereColliderDesactivado");
			}
		}*/

				 //ATTACK

        	if (Input.GetButtonDown("Fire1"))
        	{
				Atacado = true;
                anim.SetTrigger("Atacar");
                anim.SetTrigger("Idle");
        	}
       		else if (Input.GetButtonUp("Fire1"))
        	{
            	Atacado = false;
        	}
    }

    public void FixedUpdate()
    {
        if (!isDead)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (!onGround)
            {
                v = 0;
            }

            //if (h != 0 || v != 0)
            if (h > 0.5f || h < -0.5f || v > 0.5f || v < -0.5f)
            {
                body.velocity = new Vector3(h * currentSpeed, body.velocity.y, v * currentSpeed);
                anim.SetFloat ("Speed", Mathf.Abs(h * currentSpeed));
            }
            if (h == 0 && v == 0)
            {
                h = 0;
                v = 0;
                anim.SetFloat ("Speed", 0);
            }
            if (h > 0.5f && !facingRight)
            {
                Flip();
            }
            else if (h < -0.5f && facingRight)
            {
                Flip();
            }

            //SALTO FASE 1 SUBIDA
            if (jump)
            {
                jump = false;
                body.velocity = new Vector2(0, jumpForce);
            }
            
            //SALTO FASE 2 ARRIBA
                      
            if (!onGround && body.velocity.y < 2f || !onGround && body.velocity.y > -0.5f)
            {
                //ATAQUE CAIDA
                if ( Input.GetButtonDown("Fire2"))
                {
                    float velCaida = 1f;
                    body.velocity = new Vector2 (0, -velCaida);
                    anim.SetTrigger("AtaqueCaida");
                }
                //SALTO FASE 3 CAIDA
                else if (body.velocity.y < 0)
                {
                    anim.SetBool ("Land", true);
                }
            }

            HandleLayers();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Attack()
    {
    
    }



    void Escudo()
    {
        GameObject EscudoShield;
        EscudoShield = GameObject.Find("Shield(Clone)");
        if (onGround)
        {
            if (!EscudoShield)
            {
                Instantiate(shield, posEscudo.position, Quaternion.identity);
            }
            else
            {
                print("Hay Escudo");
            }
        }
    }

    /*void Stun()
          {
              //StartCoroutine(ExecuteAfterTime());
              if (CollEsfera.enabled == false)
              {
                  CollEsfera.enabled = true;
                  Debug.Log ("SphereColliderActivado");
              }
          }*/


    void PonerEscudoAmigo()
    {
        Instantiate(shield, posEscudoRanged.position, Quaternion.identity);
    }


    private void HandleLayers()
    {
        if (!onGround)
        {
            anim.SetLayerWeight (1, 1);
        }
        else
        {
            anim.SetLayerWeight (1, 0);
        }
    }

}
