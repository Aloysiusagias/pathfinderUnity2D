using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitBox_Enemy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D trigs)
    {
        if (trigs.gameObject.CompareTag("Player"))
        {
            trigs.GetComponent<Movement>().TakeDamage(20);
            Debug.Log("Kena serang!");
        }
    }
}
