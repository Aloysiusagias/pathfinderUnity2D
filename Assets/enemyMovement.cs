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
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Hurt")){
            moving();
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("turn"))
        {
            if(moveright)
            {
                moveright = false;
            }
            else
            {
               moveright = true;
            }
        }
    }

    public void TakeDamage(int Damage){
        currentHealth -= Damage;
        animator.SetTrigger("hurt");
        Debug.Log("current health" + currentHealth);
        if(currentHealth<=0){
            die();
        }
    }

    void die(){
        animator.SetBool("IsDead", true);
        Debug.Log("Enemy Die");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        this.enabled = false;
    }

    void moving(){
        if(moveright)
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
