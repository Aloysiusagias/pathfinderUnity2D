using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public bool moveright;
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    public HealthBar enemyHealthBar;
    public GameObject EnemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        enemyHealthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Hurt"))
        {
            moving();
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("turn"))
        {
            if (moveright)
            {
                EnemyHealth.transform.localScale = new Vector2(EnemyHealth.transform.localScale.x * -1, EnemyHealth.transform.localScale.y);
                moveright = false;
                Debug.Log("Positif");
            }
            else
            {
                EnemyHealth.transform.localScale = new Vector2(EnemyHealth.transform.localScale.x * -1, EnemyHealth.transform.localScale.y);
                moveright = true;
                Debug.Log("Negatif");
            }
        }
    }

    public void TakeDamage(int Damage)
    {
        currentHealth -= Damage;
        enemyHealthBar.SetHealth(currentHealth);
        animator.SetTrigger("hurt");
        Debug.Log("current health" + currentHealth);
        if (currentHealth <= 0)
        {
            die();
        }
    }

    void die()
    {
        animator.SetBool("IsDead", true);
        Debug.Log("Enemy Die");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        this.enabled = false;
        EnemyHealth.SetActive(false);
    }

    void moving()
    {
        if (moveright)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-1, 1);
        }
    }
}
