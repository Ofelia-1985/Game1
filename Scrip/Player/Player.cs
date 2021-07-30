using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  private Rigidbody2D rb2D;
  private Animator anim;
  public bool isDead;
  public int health;

  [SerializeField]
  private int speed;
  [SerializeField]
  private float jumpforce;
  [SerializeField]
  private Transform groundCheck;

  private bool facingRight;

  private int maxJump;
  public int totalJump;

  private SpriteRenderer sprite;
  private bool grounded;
  private bool jumping;
    
    //Attack
  public float attackRate;
  public Transform spawnAttack;
  public GameObject attackPrefab;
  public float nextAttack; 

  public AudioClip fxHurt;
  public AudioClip fxJump;
  public AudioClip fxAttack;

        
  void Start()
  {
    isDead = false;
    maxJump = 0;
    grounded = true;
    facingRight = true;
    nextAttack = 0f;
    rb2D = GetComponent<Rigidbody2D>(); 
    anim = GetComponent<Animator>();
    sprite = GetComponent<SpriteRenderer>();

    HudLife.instance.RefreshLife(health);
  }

  private void Update()
  {
   if(isDead)
   return;

   SetAnimations();
    
  grounded =Physics2D.Linecast(transform.position,groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

  if(Input.GetButtonDown("Jump") && maxJump>0)
   {
    AudioManager.instance.PlaySound(fxJump);
    grounded = false;
    jumping = true;
   }

   if(grounded)
   {
    maxJump =totalJump;
   }

   if(Input.GetButtonDown("Fire1") && grounded && Time.time >nextAttack)
   {
    Attack();
   }
  }

  private void FixedUpdate()
  {
    if(isDead)
      return;

    float move = Input.GetAxis("Horizontal");
    rb2D.velocity = new Vector2(move * speed, rb2D.velocity.y);

    if((move <0f && facingRight) || (move >0f && !facingRight))
    {
      facingRight = !facingRight;
      transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    if(jumping)
    {
      maxJump --;
      jumping = false;

      rb2D.velocity = new Vector2 (rb2D.velocity.x,0f);
      rb2D.AddForce (new Vector2(0f,jumpforce));
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if(isDead)
    return;

    if(other.CompareTag("Enemy"))
    {
      DamagePlayer();
    }

    if(other.CompareTag("Water"))
    {
      DamagePlayer();
    }
  }

  IEnumerator DamageEffect()

  {
      sprite.enabled = false;
      yield return new WaitForSeconds(.1f);
      sprite.enabled = true;
      yield return new WaitForSeconds(.1f);
      sprite.enabled = false;
      yield return new WaitForSeconds(.1f);
      sprite.enabled = true;
  }  

  private void DamagePlayer()
  {
    
    health--;
    AudioManager.instance.PlaySound(fxHurt);

     HudLife.instance.RefreshLife(health);

     if(health == 0)
    {
        isDead = true;
        speed = 0;
        rb2D.velocity = new Vector2(0f,0f);
        anim.SetTrigger("PlayerDead");
        Invoke("ReloadLevel",1f);
    }
     else
     {
      StartCoroutine(DamageEffect());
     }
   }
    
     public void ReloadLevel()
    {
      SceneManager.LoadScene("Scenes/SampleScene",LoadSceneMode.Single); 
    }

      private void Attack()
    {
      AudioManager.instance.PlaySound(fxAttack);
      anim.SetTrigger("Attack");
      nextAttack = Time.time + attackRate;

      GameObject cloneAtk = Instantiate(attackPrefab, spawnAttack.position,spawnAttack.rotation);

       if(!facingRight)
      {
        cloneAtk.transform.eulerAngles = new Vector3(180,0,180);
      }
    }

       private void SetAnimations()
    {
      anim.SetBool("Walk",(rb2D.velocity.x != 0f));
      anim.SetFloat("VelY", rb2D.velocity.y);
      anim.SetBool("JumpFall", !grounded);
    }
} 
