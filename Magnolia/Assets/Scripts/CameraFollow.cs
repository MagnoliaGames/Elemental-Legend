using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset;

    private void Awake()
    {
        offset = new Vector3(15, 3.5f, 0);
        transform.position = target.position + offset;
    }
    private void Start()
    {
        playerMovement = target.GetComponent<PlayerMovement>();
    }
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;

        if (playerMovement.IsGrounded == false)
        {
            offset = new Vector3(20, 1, 0);
        }
        else
        {
            offset = new Vector3(15, 3.5f, 0);
        }
    }
}
