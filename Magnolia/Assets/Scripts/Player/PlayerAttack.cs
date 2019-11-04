using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animation animationController;

    void Start()
    {
        animationController = GetComponent<Animation>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animationController.Play("Player_Attack");
        }
    }

    public bool isAttacking()
    {
        if (animationController.Play("Player_Attack"))
        {
            return true;
        }
        return false;
    }
}
