using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator animator;

    public int health = 100;
    public GameObject[] drops;

   private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            animator.SetBool("Muerto", true);
            Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("Muerte"));
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Muerte"))
            {
                Destroy(GetComponent<Rigidbody>());
                Destroy(GetComponent<CapsuleCollider>());
                StartCoroutine(Destroy());
            }       
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.15f);
        GameObject.Instantiate(drops[Random.Range(0, drops.Length)], new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), new Quaternion());
        Destroy(this.gameObject);
    }
}
