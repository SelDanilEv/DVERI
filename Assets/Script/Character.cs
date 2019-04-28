using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : Unit
{
    [SerializeField]
    private int lives = 2;
    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private float jumpForce = 15.0F;
    [SerializeField]
    public int nearCount = 5;
    [SerializeField]
    public int farCount = 5;

    public Text farCountText;

    public Text nearCountText;


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
                    livesBar1.Refresh1(lives + 1);
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
        farCountText.text = "FarBullet: " + farCount;
        nearCountText.text = "NearBullet: " + nearCount;
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


    public void buyNear()
    {
        if (Lives > 1)
        {
            Lives --;
            nearCount += 5;
        }
    }
    public void buyFar()
    {
        if (Lives > 1)
        {
            Lives--;
            farCount += 5;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) buyNear();
        if (Input.GetKeyDown(KeyCode.Alpha2)) buyFar();
        if (isGrounded) State = CharState.Idle;

        Debug.Log(nearCount);
        Debug.Log(farCount);
        if (Input.GetButtonDown("Fire1") && farCount > 0) { farCount--; farCountText.text = "FarBullet: " + farCount; Shoot(); }
        if (Input.GetButtonDown("Fire2") && nearCount > 0){ nearCount--; nearCountText.text = "NearBullet: " + nearCount; Bit(); }

        if (Input.GetButtonDown("Fire1") && farCount > 0) { farCount--; Shoot(); }
        if (Input.GetButtonDown("Fire2") && nearCount > 0) { nearCount--; Bit(); }

        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
        if (Lives <= 0)
            Debug.Log("DIe");
    }
    

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal"); //Направление движения 

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0F;

        if (isGrounded) State = CharState.Run;
    }

    private void Jump()
    {
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Shoot()
    {
        Vector3 position = transform.position; position.y += 1.0F;
        Bullet newBullet = Instantiate(bullet, position, transform.rotation) as Bullet;
        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
    }

    private void Bit()
    {
        Vector3 position = transform.position; position.y += 0.8F; position.x += 0.8F * (sprite.flipX ? -1.0F : 1.0F);
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

        isGrounded = colliders.Length > 1;

        if (!isGrounded) State = CharState.Jump;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.gameObject.GetComponent<Bullet>();
        Bullet1 bullet1 = collider.gameObject.GetComponent<Bullet1>();
        Character character = collider.GetComponent<Character>();


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