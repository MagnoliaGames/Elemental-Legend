using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private ParticleSystem psExplode, psProjectile;

    public Vector3 direction;
    public GameObject explode, projectile;
    public float speed, destroyTime;
    public int damage = 10;

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
        if (other.CompareTag("Turn") || other.CompareTag("Player") || other.CompareTag("Bullet"))
        {

        }
        else
        {
            speed = 0;
            psProjectile.Stop();
            Destroy(psProjectile);
            psExplode.Play();
            StartCoroutine(DestroyBullet());
            if (other.CompareTag("Enemy"))
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
