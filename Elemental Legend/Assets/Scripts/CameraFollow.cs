using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private PlayerMovement playerMovement;
    
    public Vector3 generalOffset { get; private set; }

    public Transform target;
    public float smoothSpeed;
    public Vector3 offsetGround, offsetJump, cameraOffset;

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
        if (playerMovement != null)
        {
            target.position = playerMovement.transform.position + cameraOffset;
            target.rotation = GameObject.Find("Erick Parent").transform.rotation;

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
}
