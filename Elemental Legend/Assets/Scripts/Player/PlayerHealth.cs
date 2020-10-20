using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool invulnerable, muerto;
    public int vidas;

    private GameObject child;
    private Gun gun;
    private Animator animator;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {        
        vidas = 3;
        muerto = false;
        invulnerable = false;
        child = GameObject.Find("Erick Child");
        gun = GetComponentInChildren<Gun>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gun == null)
        {
            gun = GetComponentInChildren<Gun>();
        }

        if (vidas <= 0)
        {
            if (gun != null)
            {
                Destroy(gun.gameObject);
            }            
            if (playerMovement.IsGrounded)
            {
                Destroy(GetComponent<PlayerMovement>());
                animator.SetLayerWeight(1, 0);
                animator.SetLayerWeight(2, 1);
                animator.SetBool("muerto", true);
                Destroy(GetComponent<Rigidbody>());
                Destroy(GetComponent<Collider>());
            }                   
            StartCoroutine(Muerte());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!invulnerable && (other.CompareTag("BulletEnemy") || other.CompareTag("Punch")))
        {
            Debug.Log("Hit Player");

            vidas -= 1;           
            StartCoroutine(Flicker());
            invulnerable = true;
        }
    }

    IEnumerator Flicker()
    {
        if (gun != null)
        {
            gun.canShoot = false;
        }
        for (int i = 0; i < 10; i++)
        {
            child.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            child.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        invulnerable = false;
        if (gun != null)
        {
            gun.canShoot = true;
        }
    }

    IEnumerator Muerte()
    {
        yield return new WaitForSeconds(2);
        muerto = true;
    }
}
