using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private SwordAttack swordAttack;

    public int damage = 10;

    private void Start()
    {
        swordAttack = GetComponent<SwordAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy") && swordAttack.isAttacking)
        {
            Debug.Log("hit enemy");
            other.GetComponent<EnemyHealth>().health -= damage;
        }
    }
}
