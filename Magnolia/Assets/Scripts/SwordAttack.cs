using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private Animator animator;
    public bool isAttacking;

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
}
