using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{  
    private Rigidbody m_Rigidbody;
    private Vector3 gravity;
    private int jumps;

    public bool IsGrounded { get; private set; }

    public bool doubleJump;
    public float gravityForce = -9.81f;
    public float movementSpeed = 10;
    public float jumpForce = 15;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        gravity = new Vector3(0, gravityForce, 0);
        IsGrounded = true;
    }

    void FixedUpdate()
    {
        m_Rigidbody.AddForce(gravity, ForceMode.Acceleration);

        if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1, 1, 1);
            //transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.position += transform.forward * movementSpeed * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1, 1, -1);
            //transform.localEulerAngles = new Vector3(0, 180, 0);
            transform.position += -transform.forward * movementSpeed * Time.fixedDeltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        //if (!IsGrounded && transform.position.y >5)
        //{
        //    m_Rigidbody.velocity = Vector3.up * -1;
        //}
    }

    private void Jump()
    {       
        if (IsGrounded)
        {
            jumps = 0;
        }
        if (IsGrounded || (doubleJump && jumps < 2))
        {
            m_Rigidbody.velocity = Vector3.up * jumpForce;
            jumps += 1;
            IsGrounded = false;
        }      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
        {
            IsGrounded = true;
        }
    }
}
