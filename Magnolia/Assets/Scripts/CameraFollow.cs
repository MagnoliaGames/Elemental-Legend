using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public Transform target;
    public float smoothSpeed = 3f;
    public Vector3 offset;

    private void Awake()
    {
        transform.position = target.position + offset;
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
        Vector3 desiredPosition = target.TransformPoint(offset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;

        if (playerMovement.IsGrounded == false)
        {
            offset = new Vector3(15, 1.5f, 0);
        }
        else
        {
            offset = new Vector3(10, 1.5f, 0);
        }
    }
}
