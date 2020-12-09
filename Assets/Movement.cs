using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake(){
        rb = transform.GetComponent<Rigidbody2D>();
        bc = transform.GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        HandleInput();
        if(!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attacks")){
            HandleMovement();
        }
        HandleAttack();
        horizontalMove = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        ResetValue();
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
        if(attacking && !this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attacks")){
            animator.SetTrigger("Attacks");
            rb.velocity = Vector2.zero;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach(Collider2D enemy in hitEnemies){
                enemy.GetComponent<enemyMovement>().TakeDamage(40);
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
}
