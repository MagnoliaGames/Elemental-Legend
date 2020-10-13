using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator animator;

    public int health = 100;
    public bool muerto, bulletHit, granadeHitable;
    public GameObject[] drops;

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
            LevelManager.puntuacion += 100;
            animator.SetBool("Muerto", true);
            if (GetComponentInChildren<GunEnemy>())
            {
                Destroy(GetComponentInChildren<GunEnemy>().gameObject);
            }           
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<CapsuleCollider>());
            StartCoroutine(Destroy());      
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
            health -= other.GetComponent<Bullet>().damage;
            bulletHit = true;
            StartCoroutine(ResetBulletHit());
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
        GameObject.Instantiate(drops[Random.Range(0, drops.Length)], new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), new Quaternion());
        Destroy(this.gameObject);
    }

    IEnumerator Hitable()
    {
        yield return new WaitForSeconds(1f);
        granadeHitable = true;
    }
}
