using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PlayerAttack playerAttack;

    public int damage = 10;

    private void Start()
    {
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy") && playerAttack.isAttacking)
        {
            other.GetComponent<EnemyHealth>().health -= damage;
        }
    }
}
