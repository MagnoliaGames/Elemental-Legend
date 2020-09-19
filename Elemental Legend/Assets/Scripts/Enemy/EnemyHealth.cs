using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator animator;

    public int health = 100;
    public bool muerto, granadeHitable;
    public GameObject[] drops;

   private void Start()
    {
        muerto = false;
        granadeHitable = true;
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            muerto = true;
            animator.SetBool("Muerto", true);
            Destroy(GetComponentInChildren<GunEnemy>().gameObject);
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<CapsuleCollider>());
            StartCoroutine(Destroy());      
        }

        if (!granadeHitable)
        {
            StartCoroutine(Hitable());
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.15f);
        GameObject.Instantiate(drops[Random.Range(0, drops.Length)], new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), new Quaternion());
        Destroy(this.gameObject);
    }

    IEnumerator Hitable()
    {
        yield return new WaitForSeconds(0.5f);
        granadeHitable = true;
    }
}
