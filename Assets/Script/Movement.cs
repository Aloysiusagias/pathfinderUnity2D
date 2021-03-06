﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    public Animator animator;
    float horizontalMove;
    private bool attacking;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int maxHealth = 100;
    static public int currentHealth = 100;
    public HealthBar healthhbar;
    public GameObject Retry,Quit;
    

    // Start is called before the first frame update
    void Start()
    {
        healthhbar.SetMaxHealth(maxHealth, currentHealth);
    }

    void Awake(){
        rb = transform.GetComponent<Rigidbody2D>();
        bc = transform.GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        HandleInput();
        if(!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attacks") && !this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Hurts")){
            HandleMovement();
        }
        HandleAttack();
        horizontalMove = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        ResetValue();
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("turn"))
        {
            currentHealth -= 20;
            Debug.Log(currentHealth);
            healthhbar.SetHealth(currentHealth);
        }
    }

    private void HandleMovement(){
        float moveSpeed = 10f;
        if(Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            rb.transform.localScale = new Vector3(-1, rb.transform.localScale.y, rb.transform.localScale.z);
        } else if (Input.GetKey(KeyCode.RightArrow)){
            rb.velocity = new Vector2(+moveSpeed, rb.velocity.y);
            rb.transform.localScale = new Vector3(1, rb.transform.localScale.y, rb.transform.localScale.z);
        } else {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void HandleAttack(){
        if(attacking && !this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attacks") && !this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Hurts")){
            animator.SetTrigger("Attacks");
            rb.velocity = Vector2.zero;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach(Collider2D enemy in hitEnemies){
                enemy.GetComponent<Enemy_behaviour>().TakeDamage(40);
            }
        }
    }

    private void HandleInput(){
        if(Input.GetKeyDown(KeyCode.Z)){
            attacking = true;
        }
    }

    private void ResetValue(){
        attacking = false;
    }

    void OnDrawGizmosSelected(){
        if(attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void TakeDamage(int Damage)
    {
        currentHealth -= Damage;
        Debug.Log(currentHealth);
        healthhbar.SetHealth(currentHealth);
        animator.SetTrigger("Hurt");
        if(currentHealth <= 0){
            die();
        }
    }

    void die()
    {
        animator.SetBool("isDead", true);
        // Debug.Log("Player Die");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        this.enabled = false;
        Retry.SetActive(true);
        Quit.SetActive(true);

        // EnemyHealth.SetActive(false);
    }
}