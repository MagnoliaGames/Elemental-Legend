using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    private ParticleSystem psExplode, psFire;

    public GameObject explode, fire;
    public float speed, explosionRadius, timeExplosion;
    public int damage;

    void Start()
    {
        psExplode = explode.GetComponent<ParticleSystem>();
        psFire = fire.GetComponent<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();

        psFire.Play();
        rb.AddForce((player.transform.forward + Vector3.up) * speed, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        StartCoroutine(DestroyMolotovNoCollision());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("PlayerTurns"))
        {          
            psFire.Stop();
            psExplode.Play();                        
            GameObject.Find("MolotovChild").GetComponent<MeshRenderer>().enabled = false;
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
            StopCoroutine(DestroyMolotovNoCollision());
            StartCoroutine(DestroyMolotov());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    IEnumerator DestroyMolotov()
    {
        AudioSource audio = GetComponentInChildren<AudioSource>();
        if (!audio.isPlaying && audio != null)
        {
            audio.Play();
        }
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    IEnumerator DestroyMolotovNoCollision()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
