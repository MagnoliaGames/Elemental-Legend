using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMovement : MonoBehaviour
{
    public GameObject rightHand;
    public bool turned, warningState;
    public float movementSpeed;

    private Animator animator;
    private GameObject player;
    private EnemyHealth health;
    private float speed;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = GetComponentInParent<EnemyHealth>();

        warningState = false;
    }

    void FixedUpdate()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<Collider>())
            {
                Physics.IgnoreCollision(GetComponentInParent<Collider>(), enemy.GetComponent<Collider>());
            }
        }

        Vector3 relativePlayer = transform.InverseTransformPoint(player.transform.position);
        if (relativePlayer.z < 0.0f && turned)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 180, transform.localEulerAngles.z);
            turned = false;
        }
        else if (relativePlayer.z < 0.0f && !turned)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 180, transform.localEulerAngles.z);
            turned = true;
        }

        if (relativePlayer.z < 0.0f)
        {
            health.transform.position -= transform.forward * speed * Time.fixedDeltaTime;
        }
        else if(relativePlayer.z > 0.0f)
        {
            health.transform.position += transform.forward * speed * Time.fixedDeltaTime;
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= 1)
        {
            if (!health.muerto)
            {
                speed = 0;
                animator.SetBool("Ataque", true);
                rightHand.GetComponent<Collider>().enabled = true;
                AudioSource audio = rightHand.GetComponent<AudioSource>();
                if (!audio.isPlaying)
                {
                    audio.Play();
                }
            }             
        }
        else
        {
            speed = movementSpeed;
            animator.SetBool("Ataque", false);
            rightHand.GetComponent<Collider>().enabled = false;
        }

        if (health.muerto)
        {
            movementSpeed = 0;
        }
    }
}
