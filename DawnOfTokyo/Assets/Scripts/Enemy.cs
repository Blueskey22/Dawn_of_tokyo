using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

  private int health = 100;
  private float movementSpeed = 0.75f;
  public Transform playerPos;
  private Animator anim;
  PlayerController2D scriptPlayer;
  public GameObject Player;
  RaycastHit2D hitAtaque;
  private bool facingRight = true;

    [SerializeField]
    private float contador;
    [SerializeField]
    private bool  ha_Atacado = false;
    [SerializeField]
    public bool ha_Dado = false;


   // public GameObject Pos_personaje;
void Start()
{
    anim = GetComponent<Animator>();
    scriptPlayer = Player.GetComponent<PlayerController2D>(); 
}

void Update()
{
   
}

void FixedUpdate()
{
     Movement();
}


 void TakeDamage(int damageAmount)
 {
     health = health - damageAmount;
 
     // We should also check if the health is still greater than 0 
     // in order to determine whether enemy is still alive or not
 
     if(health < 0)
     {
         Debug.Log ("EnemigoMuerto");
     }
 }

 void Movement()
 {
     if(scriptPlayer.isDead == false)
     {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, movementSpeed *Time.deltaTime);
        anim.SetFloat("Speed", 1);
         if(transform.position.x <= playerPos.position.x && !facingRight)
         {
            Flip();
         }
         else if(transform.position.x >= playerPos.position.x && facingRight)
         {
            Flip();
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

public void Ataque()
{
    contador = contador + Time.deltaTime;
    if (scriptPlayer.isDead == false)
    {
        if (!ha_Atacado && contador >= 0.009f)
        {
            print ("Hola");
            anim.SetTrigger("Atacar");
            if (ha_Dado)
            {
            contador = 0f;
            }
            ha_Atacado = true; 
        }
        else
        {
            return;
        }
    }
    print("J");
    anim.SetTrigger("Idle");
}



}
