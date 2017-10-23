using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float maxSpeed = 4f;
    public float jumpForce = 5f;
    public Sprite spriteAtaque;
    public Sprite spriteAndar;
    public SpriteRenderer sr;
    public Transform posEscudo;
    public Transform posEscudoRanged;
    public GameObject shield;
//    float tiempo = 0f;


    
    private float currentSpeed;
    private Rigidbody body;
    private Animator anim;
    private bool onGround;
    private bool isDead = false;
    private bool facingRight = true;
    private bool jump = false;
    //private SphereCollider CollEsfera;
    //private BoxCollider ColliderAtaque;
	//private bool ColliderAtaqueActivado = false;

    //VariablesAtaque
//    float damage = 5f;
    bool Atacado = false;


    //private bool IsCourutineExecuting = false;


    //private int hitRange = 10;

    RaycastHit hit;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
        anim = GetComponentInChildren <Animator>();
        sr = GetComponent<SpriteRenderer>();
        //CollEsfera = GetComponent<SphereCollider>();
        //ColliderAtaque = GetComponent<BoxCollider>();
        //ColliderAtaque.enabled = true;
        currentSpeed = maxSpeed;
        //CollEsfera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 8;


        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, 5, layerMask))
        {
            onGround = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        else
        {
            onGround = true;
        }


        float j = Input.GetAxis("Jump");
        if (j > 0 && onGround)
        {
            jump = true;
        }

       

        //SHIELD

        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Escudo");
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
           	 	//sr.sprite = spriteAtaque;
            	//Attack();
				Atacado = true;
				Animaciones();
        	}
       		else if (Input.GetButtonUp("Fire1"))
        	{
            	//sr.sprite = spriteAndar;
            	Atacado = false;
        	}
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (!onGround)
            {
                v = 0;
            }

            if (h != 0 || v != 0)
            {
                body.velocity = new Vector3(h * currentSpeed, body.velocity.y, v * currentSpeed);
                sr.sprite = spriteAndar;
            }
            if (h == 0 && v == 0)
            {
                h = 0;
                v = 0;
            }
            if (h > 0 && !facingRight)
            {
                Flip();
            }
            else if (h < 0 && facingRight)
            {
                Flip();
            }

            if (jump)
            {
                jump = false;
                body.velocity = new Vector3(0, jumpForce, 0);
            }
	

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
    	Animaciones();
    }

 


    //LO SIGUIENTE VA DENTRO DE ATAQUE
    /*

            int layerMask1 = 1 << 9;

            //NO ENTRA EN EL if
            if (Physics.Raycast(origen, forward, out hit1, 5, layerMask1)) 
            {
            Debug.DrawRay(origen, forward * hit1.distance, Color.black);
            Debug.Log("Did Hit");
            }

            if (Physics.Raycast(origen, forward, out hit1, hitRange))
            {
                Debug.Log("primer IF");
                if (transform.gameObject.tag == "Enemy")
                {
                    transform.gameObject.SendMessage("TakeDamage", 30);
                    Debug.Log("Ha dado a enemigo");
                }
            }	
            Debug.Log ("ya");*/
    //HASTA AQUI

    void Escudo()
    {
        GameObject EscudoShield;
        EscudoShield = GameObject.Find("Shield(Clone)");
        if (!EscudoShield)
        {
            Instantiate(shield, posEscudo.position, Quaternion.identity);
        }
        else
        {
            print("Hay Escudo");
        }

    }

    /*
        IEnumerator ExecuteAfterTime ()
        {
            double tiempo = 0.1;
            if (IsCourutineExecuting)
            yield break;

         IsCourutineExecuting = true;

         yield return new WaitForSeconds(2);

         // Code to execute after the delay

         IsCourutineExecuting = false;
            if (CollEsfera.enabled == false)
            {
                CollEsfera.enabled = true;
            }
            if (CollEsfera.enabled == true || Time.frameCount > tiempo)
            {
                CollEsfera.enabled = false;
                Debug.Log("si");
            }

        }
    */    /*void Stun()
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





    //ANIMACIONES
	void Animaciones ()
	{
		if (Atacado)
		{
			anim.SetTrigger("Atacar");
		} 
		anim.SetTrigger("Idle");
	}


}
