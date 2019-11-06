using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    public bool isAttacking;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }   
}
