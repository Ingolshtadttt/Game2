using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hero : Unit
{
    [SerializeField] private GameController gameController;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int health;
    [SerializeField] private int lives;
    [SerializeField] private float jumpForce = 15f;
    private bool isGrounded = false;

    public bool isAttacking = false;
    public bool isRecharged = true;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;

    [SerializeField] private Image[] hearts;

    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;


    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }//возвращает текущий State в аниматоре
        set { animator.SetInteger("State", (int)value); }//записывает значение в аниматор
    }

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    private void Awake()
    {
        lives = 5;
        health = lives;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        isRecharged = true;
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
        if (isGrounded) State = CharState.Run;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Attack()
    {
        if (isGrounded && isRecharged)
        {
            State = CharState.Attack;
            isAttacking = true;
            isRecharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
    }

    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Unit>().ReceiveDamage();
        }
    }
    
    public override void ReceiveDamage()
    {
        health--;
        if (health < 1)
        {
            foreach (var h in hearts)
                h.sprite = deadHeart;
            Die();
        }

        if (health > 0)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.up * 11.0F, ForceMode2D.Impulse);
            Debug.Log(health);
        }       
    }

    public override void Die()
    {
        State = CharState.Death;
        //Destroy(gameObject, 3.3f);
        //SceneManager.LoadScene(0);
        //LevelManager.instance.Respawn();
        gameController.LoseGame();

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private IEnumerator AttackAnimation()//time to attack
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }

    private IEnumerator AttackCoolDown()//time to reload
    {
        yield return new WaitForSeconds(0.5f);
        isRecharged = true;
    }

    private void CheckGround()
    {
        Collider2D[] colider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = colider.Length > 0;
        if (!isGrounded) State = CharState.Jump;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.tag=="platform")
        {
            this.transform.parent = col.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "platform")
        {
            this.transform.parent = null ;
        }
    }


    private void Update()
    {
        if (isGrounded && !isAttacking) State = CharState.Idle;
        if (!isAttacking && Input.GetButton("Horizontal"))
            Run();
        if (!isAttacking && isGrounded && Input.GetButtonDown("Jump"))
            Jump();
        if (Input.GetButtonDown("Fire1"))
            Attack();
        if (health < 1) State = CharState.Death;
   /*     if (transform.position.y < -20f)
            Die();*/
        if (health > lives)
            health = lives;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = aliveHeart;
            else
                hearts[i].sprite = deadHeart;

            if (i < lives)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }
}

public enum CharState
{
    Idle,
    Run,
    Jump,
    Attack,
    Death
}
