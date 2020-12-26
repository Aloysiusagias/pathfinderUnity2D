using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behaviour : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance; //Minimum distance for attack
    public float moveSpeed;
    public float timer;
    public Transform leftLimit;
    public Transform rightLimit;
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private Transform target;
    private Animator anim;
    private float distance; //Store the distance between enemy and player
    private bool attackMode;
    private bool inRange; //Check if Player is in range
    private bool cooling; //Check if Enemy is cooling after attack
    private float intTimer;
    #endregion

    public int maxHealth = 100;
    int currentHealth;
    public GameObject parent;
    public HealthBar enemyHealthBar;
    public GameObject EnemyHealth;

    void Awake()
    {
        SelectTarget();
        intTimer = timer; //Store the initial value of timer
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        enemyHealthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > 0)
        {
            if (!attackMode)
            {
                Move();
            }

            if (!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("attacking"))
            {
                SelectTarget();
            }

            if (inRange)
            {
                hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, raycastMask);
                RaycastDebugger();
            }

            //when player is detected
            if (hit.collider != null)
            {
                enemyLogic();
            }
            else if (hit.collider == null)
            {
                inRange = false;
            }

            if (inRange == false)
            {
                StopAttack();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D trigs)
    {
        if (trigs.gameObject.CompareTag("Player"))
        {
            target = trigs.transform;
            inRange = true;
            Flip();
        }
    }

    void enemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attacking"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    void RaycastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    private void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
            // EnemyHealth.transform.localScale = new Vector2(EnemyHealth.transform.localScale.x * -1, EnemyHealth.transform.localScale.y);
        }
        else
        {
            rotation.y = 0f;
            // EnemyHealth.transform.localScale = new Vector2(EnemyHealth.transform.localScale.x * -1, EnemyHealth.transform.localScale.y);
        }

        transform.eulerAngles = rotation;
    }
    public void TakeDamage(int Damage)
    {
        currentHealth -= Damage;
        enemyHealthBar.SetHealth(currentHealth);
        anim.SetTrigger("hurt");
        Debug.Log("current health : " + currentHealth);
        if (currentHealth <= 0)
        {
            die();
        }
    }
    void die()
    {
        anim.SetBool("isDead", true);
        Debug.Log("Enemy Die");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        parent.transform.GetChild(0).gameObject.SetActive(false);
        parent.transform.GetChild(2).gameObject.SetActive(false);
        this.enabled = false;
        EnemyHealth.SetActive(false);
    }
}
