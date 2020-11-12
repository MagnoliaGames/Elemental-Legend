using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponentInChildren<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Piso") && playerMovement != null)
        {
            playerMovement.GetComponent<Animator>().SetBool("jump", false);
            playerMovement.IsGrounded = true;
        }
    }
}
