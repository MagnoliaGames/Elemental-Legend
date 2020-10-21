﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator animator;

    public int puntuacion;
    public int health = 100;
    public bool muerto, bulletHit, granadeHitable;

   private void Start()
    {
        muerto = false;
        bulletHit = false;
        granadeHitable = true;
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (health <= 0 && !muerto)
        {
            muerto = true;
            LevelManager.puntuacion += puntuacion;
            if (animator != null)
            {
                animator.SetBool("Muerto", true);
                if (GetComponentInChildren<GunEnemy>())
                {
                    Destroy(GetComponentInChildren<GunEnemy>().gameObject);
                }
                Destroy(GetComponent<Rigidbody>());
                Destroy(GetComponent<CapsuleCollider>());
                StartCoroutine(Destroy());
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        if (!granadeHitable)
        {
            StartCoroutine(Hitable());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (this.GetComponent<Panal>())
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), other);
            }
            else
            {
                health -= other.GetComponent<Bullet>().damage;
                bulletHit = true;
                StartCoroutine(ResetBulletHit());
            }           
        }
    }

    IEnumerator ResetBulletHit()
    {
        yield return new WaitForSeconds(5f);
        bulletHit = false;
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.15f);
        Destroy(this.gameObject);
    }

    IEnumerator Hitable()
    {
        yield return new WaitForSeconds(1f);
        granadeHitable = true;
    }
}
