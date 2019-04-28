using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    [SerializeField]
    private int lives = 2;
    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private float jumpForce = 15.0F;

    

    public int Lives
    {

        get { return lives; }
        set
        {
            int temp = lives;
            if (value < 5)
                lives = value;
            
                Debug.Log(lives);
            if ((lives % 2 == 1) || temp < lives) // если нечетное
            {
                if (lives % 2 == 0 && temp < lives) //если подобрали жизнь и четная
                {
                    livesBar.Refresh(lives + 1);
                }
                else if (lives % 2 == 1 && temp < lives) //если подобрали жизнь и нечетная
                {
                    livesBar1.Refresh1(lives+1);
                }
                livesBar.Refresh(lives);
            }
            else // если четное
                livesBar1.Refresh1(lives);
            
        }

    }

    private LivesBare livesBar;

    private LivesBare1 livesBar1;

    private bool isGrounded = false;

    private Bullet bullet;
    private Bullet1 bullet1;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    new private Rigidbody2D rigidbody;
    private Animator animator; //Анимация 
    private SpriteRenderer sprite; // Поворот 


    private void Awake()
    {
        livesBar = FindObjectOfType<LivesBare>();
        livesBar1 = FindObjectOfType<LivesBare1>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
        bullet1 = Resources.Load<Bullet1>("Bullet1");
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if(isGrounded) State = CharState.Idle;

        if (Input.GetButtonDown("Fire1")) Shoot();
        if (Input.GetButtonDown("Fire2")) Bit();
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
        if (Lives <= 0)
            Debug.Log("DIe");
           // Destroy(gameObject);

    }
        
    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal"); //Направление движения 

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0F;

       if(isGrounded) State = CharState.Run;
    }

    private void Jump()
    {
        rigidbody.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
    }

    private void Shoot()
    {
        
        Vector3 position = transform.position; position.y += 1.0F;
        Bullet newBullet =  Instantiate(bullet, position, transform.rotation) as Bullet;
        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
    }

    private void Bit()
    {
        Vector3 position = transform.position; position.y += 0.8F;position.x += 0.8F* (sprite.flipX ? -1.0F : 1.0F);
        Bullet1 newBullet1 = Instantiate(bullet1, position, transform.rotation) as Bullet1;
        newBullet1.Parent = gameObject;
        newBullet1.Direction = newBullet1.transform.right * (sprite.flipX ? -1.0F : 1.0F);
    }

    public override void ReceiveDamage()
    {
        Lives--;
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 8.0F, ForceMode2D.Impulse);
        Debug.Log(lives);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,0.3F);

        isGrounded = colliders.Length > 1;

        if (!isGrounded) State = CharState.Jump;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.gameObject.GetComponent<Bullet>();
        Bullet1 bullet1 = collider.gameObject.GetComponent<Bullet1>();

        if (bullet && bullet.Parent != gameObject || bullet1 && bullet1.Parent != gameObject)
            ReceiveDamage();
    }

}

public enum CharState
{
    Idle,
    Run,
    Jump
}