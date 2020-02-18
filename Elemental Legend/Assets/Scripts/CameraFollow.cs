using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Vector3 generalOffset;

    public Transform target;
    public float smoothSpeed = 3f;
    public Vector3 offsetGround, offsetJump;

    private void Awake()
    {
        generalOffset = offsetGround;
        transform.position = target.position + generalOffset;
    }

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        target.position = playerMovement.transform.position;
        target.rotation = playerMovement.transform.rotation;

        transform.LookAt(target);
        Vector3 desiredPosition = target.TransformPoint(generalOffset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;

        if (playerMovement.IsGrounded == false)
        {
            generalOffset = offsetJump;
        }
        else
        {
            generalOffset = offsetGround;
        }
    }
}
