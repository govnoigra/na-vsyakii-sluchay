using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterInCampaighController : Unit
{
    [SerializeField]   //чтобы приватные отображались в инспекторе
    private int lives = 5; //жизни
    [SerializeField]
    private float speed = 3.0f; //скорость
    [SerializeField]
    private float jumpForce = 15.0f; //сила прыжка

    private bool isGrounded = false; //на земле ли персонаж (для прыжков)
    public bool canDoubleJump; //для двойного прыжка

    public int directionInput; //в какую сторону нужно идти

    public bool facingRight = true; //сторона поворота персонажа

    public GameObject Player; //сам дракон
    public float xPlayer, yPlayer; //координаты дракона

    private Fire fire; //огонь для выстрела
    private int fireDirection = 1;
    
    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer dragon;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dragon = GetComponentInChildren<SpriteRenderer>();

        fire = Resources.Load<Fire>("Fire"); //подгружаем префаб огня для выстрела
    }


    private void FixedUpdate()
    {
        CheckGround();
    }

    
    private void Update()
    {
        if (isGrounded) State = CharState.Idle;
        RunUI();

        xPlayer = Player.transform.position.x;
        yPlayer = Player.transform.position.y;
        if ((directionInput < 0) && (facingRight))
        {
            Flip();
            xPlayer += (float)2.5;
            Player.transform.position = new Vector3(xPlayer, yPlayer, 0f);

        }
        if ((directionInput > 0) && (!facingRight))
        {
            Flip();
            xPlayer -= (float)2.5;
            Player.transform.position = new Vector3(xPlayer, yPlayer, 0f);
        }
        if (isGrounded && directionInput!=0)
        {
            State = CharState.Run;
        }

    }

    void Flip()  //поворот персонажа
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


      public void RunUI()
      {
        
        Vector3 direction = transform.right * directionInput;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    //    dragon.flipX = direction.x < 0.0f;
       // if (isGrounded) State = CharState.Run;
    } 

   public void Move(int InputAxis)
    {
        directionInput = InputAxis;
        if (directionInput != 0) fireDirection = directionInput;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            canDoubleJump = true;
        } else if (canDoubleJump)
        {
            rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            canDoubleJump = false;
        }

    }

    public void Shoot()
    {
        Vector3 position = transform.position;
         position.y -= 0.2F;
        if (fireDirection > 0) position.x += 4.5F;
        else if (fireDirection < 0) position.x -= 3.0F;
        Fire newFire = Instantiate(fire, position, fire.transform.rotation) as Fire;

        newFire.Direction = newFire.transform.right * fireDirection;
        if (isGrounded) State = CharState.Attack;
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5F);
        isGrounded = colliders.Length > 1; // если кол-во попадающих коллайдеров больше 1, то игрок стоит на земле
        if (!isGrounded) State = CharState.Jump;
    }
    
}

public enum CharState
{
    Idle,
    Run,
    Jump,
    Attack
}