using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    private ParticleSystem psExplode;

    public GameObject explode;
    public float speed, explosionRadius, timeExplosion;
    public int damage;

    void Start()
    {
        psExplode = explode.GetComponent<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        rb.AddForce((player.transform.forward + Vector3.up) * speed, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        StartCoroutine(ExplodeGranade());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    IEnumerator ExplodeGranade()
    {
        yield return new WaitForSeconds(timeExplosion);
        psExplode.Play();
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Enemy"))
            {
                GameObject enemy = nearbyObject.gameObject;
                if (enemy.GetComponent<EnemyHealth>().granadeHitable)
                {
                    Debug.Log("hit enemy");
                    enemy.GetComponent<EnemyHealth>().health -= damage;
                    enemy.GetComponent<EnemyHealth>().granadeHitable = false;                    
                }              
            }
        }
        StartCoroutine(DestroyGranade());
    }

    IEnumerator DestroyGranade()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}