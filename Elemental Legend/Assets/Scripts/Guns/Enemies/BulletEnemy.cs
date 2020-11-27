﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private float normalization;
    private Vector3 normalizedOrientation;
    private Rigidbody rb;
    private ParticleSystem psExplode, psProjectile;

    public Vector3 direction;
    public GameObject explode, projectile;
    public float speed, destroyTime;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        psExplode = explode.GetComponent<ParticleSystem>();
        psProjectile = projectile.GetComponent<ParticleSystem>();
        psProjectile.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector3(transform.forward.x, transform.forward.y, transform.forward.z) * speed;
        StartCoroutine(DestroyBullet());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Turn") || other.CompareTag("Enemy") || other.CompareTag("Bullet") || other.CompareTag("Abejas") || other.CompareTag("Victoria"))
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), other);
        }
        else
        {
            speed = 0;
            if (psProjectile != null)
            {
                psProjectile.Stop();
                Destroy(projectile);
            }
            if (psExplode != null)
            {
                psExplode.Play();
            }
            StartCoroutine(DestroyBullet());
            if (other.CompareTag("PlayerTurns"))
            {
                Destroy(GetComponent<Collider>());
            }
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
