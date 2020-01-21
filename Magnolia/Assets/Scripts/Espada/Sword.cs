using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Animator animator;

    public bool isAttacking;
    public int damage = 10;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isAttacking == false)
        {
            animator.SetTrigger("Attack");
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SwordAttack"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy") && isAttacking)
        {
            Debug.Log("hit enemy");
            other.GetComponent<EnemyHealth>().health -= damage;
        }
    }
}
