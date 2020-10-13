using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nativo : MonoBehaviour
{
    private GameObject player;
    private Animator animator;

    public bool libre;
    public float rangoInteraccion, movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        libre = false;

        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < rangoInteraccion && Input.GetKeyDown(KeyCode.E))
        {
            libre = true;
        }

        if (libre && !animator.GetBool("Walk"))
        {
            transform.localEulerAngles = transform.localEulerAngles - new Vector3(0, -90, 0);
            animator.SetBool("Walk", true);
            LevelManager.puntuacion += 1000;
            StartCoroutine(DestroyNativo());
        }
        if (animator.GetBool("Walk"))
        {
            transform.position += transform.forward * movementSpeed * Time.fixedDeltaTime;
        }
    }

    IEnumerator DestroyNativo()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoInteraccion);
    }
}
