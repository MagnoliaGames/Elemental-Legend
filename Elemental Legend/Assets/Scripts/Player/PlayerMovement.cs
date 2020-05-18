using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{  
    private Rigidbody m_Rigidbody;
    private Vector3 gravity;
    private CameraFollow cameraFollow;
    private Gun gun;
    private int cont = 0;
    private int jumps;

    public bool IsGrounded { get; private set; }

    public bool turned;
    public bool doubleJump;
    public float gravityForce = -9.81f;
    public float movementSpeed = 10;
    public float jumpForce = 15;
    public Transform mouse;

    void Start()
    {
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();      
        m_Rigidbody = GetComponent<Rigidbody>();
        gravity = new Vector3(0, gravityForce, 0);
        IsGrounded = true;
    }

    void FixedUpdate()
    {
        if (gun == null)
        {
            gun = GetComponentInChildren<Gun>();
        }

        mouse.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraFollow.generalOffset.x));

        m_Rigidbody.AddForce(gravity, ForceMode.Acceleration);

        if (Input.GetKey(KeyCode.D))
        {
            turned = false;
            transform.position += transform.forward * movementSpeed * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            turned = true;
            transform.position += -transform.forward * movementSpeed * Time.fixedDeltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        LookMouse(); 
    }

    private void LookMouse()
    {
        if (turned == false && mouse.localPosition.z < 0)
        {
            if (cont == 0)
            {
                transform.localScale = new Vector3(-1, 1, -1);
                gun.transform.localScale = new Vector3(1f, 1f, -1f);
                gun.roty = 180;
                cont = 1;
            }
            else if (cont == 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
                gun.transform.localScale = new Vector3(1f, 1f, 1f);
                gun.roty = 0;
                cont = 0;
            }

        }
        else if (turned == true && mouse.localPosition.z < 0)
        {
            if (cont == 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                gun.transform.localScale = new Vector3(1f, 1f, 1f);
                gun.roty = 0;
                cont = 1;
            }
            else if (cont == 1)
            {
                transform.localScale = new Vector3(-1, 1, -1);
                gun.transform.localScale = new Vector3(1f, 1f, -1f);
                gun.roty = 180;
                cont = 0;
            }
        }
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
