using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator animator;

    public GameObject drop;
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
                if (this.gameObject.GetComponent<Panal>().abejasLiberadas != null)
                {
                    Flock flock = GetComponent<Panal>().abejasLiberadas;
                    flock.destroy = true;
                }
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
        if (GetComponentInChildren<GolemMovement>())
        {
            GetComponent<AudioSource>().Play();
        }
        yield return new WaitForSeconds(1.15f);
        if (GetComponentInChildren<GolemMovement>() && drop != null)
        {
            Instantiate(drop, this.transform.position, GetComponentInChildren<GolemMovement>().transform.rotation);
        }
        Destroy(this.gameObject);
    }

    IEnumerator Hitable()
    {
        yield return new WaitForSeconds(1f);
        granadeHitable = true;
    }
}
