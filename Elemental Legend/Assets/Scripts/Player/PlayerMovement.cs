using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject erickChild;
    private Rigidbody m_Rigidbody;
    private Vector3 gravity;
    private CameraFollow cameraFollow;
    private Animator animator;
    private Gun gun;
    private int jumps;

    public bool IsGrounded { get; private set; }

    public bool turned;
    public bool doubleJump;
    public float gravityForce = -9.81f;
    public float movementSpeed = 10;
    public float jumpForce = 15;
    public Transform mouse, ikRight, ikLeft;

    void Start()
    {
        erickChild = GameObject.Find("Erick Child");
        m_Rigidbody = GetComponent<Rigidbody>();
        gravity = new Vector3(0, gravityForce, 0);
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        animator = GetComponent<Animator>();
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
            //turned = false;
            transform.position += transform.forward * movementSpeed * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //turned = true;
            transform.position += -transform.forward * movementSpeed * Time.fixedDeltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        LookMouse(); 
    }

    private void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);

        animator.SetIKPosition(AvatarIKGoal.RightHand, ikRight.position);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, ikLeft.position);
    }

    private void LookMouse()
    {
        if (mouse.localPosition.z < 0 && !turned)
        {
            erickChild.transform.localEulerAngles = new Vector3(erickChild.transform.localEulerAngles.x, erickChild.transform.localEulerAngles.y + 180, erickChild.transform.localEulerAngles.z);
            turned = true;
        }
        else if (mouse.localPosition.z < 0 && turned)
        {
            turned = false;
            erickChild.transform.localEulerAngles = new Vector3(erickChild.transform.localEulerAngles.x, erickChild.transform.localEulerAngles.y + 180, erickChild.transform.localEulerAngles.z);
        }

        //if (turned == false && mouse.localPosition.z < 0)
        //{
        //    if (cont == 0)
        //    {
        //        transform.localScale = new Vector3(-1, 1, -1);
        //        gun.transform.localScale = new Vector3(1f, 1f, -1f);
        //        gun.roty = 180;
        //        cont = 1;
        //    }
        //    else if (cont == 1)
        //    {
        //        transform.localScale = new Vector3(1, 1, 1);
        //        gun.transform.localScale = new Vector3(1f, 1f, 1f);
        //        gun.roty = 0;
        //        cont = 0;
        //    }

        //}
        //else if (turned == true && mouse.localPosition.z < 0)
        //{
        //    if (cont == 0)
        //    {
        //        transform.localScale = new Vector3(1, 1, 1);
        //        gun.transform.localScale = new Vector3(1f, 1f, 1f);
        //        gun.roty = 0;
        //        cont = 1;
        //    }
        //    else if (cont == 1)
        //    {
        //        transform.localScale = new Vector3(-1, 1, -1);
        //        gun.transform.localScale = new Vector3(1f, 1f, -1f);
        //        gun.roty = 180;
        //        cont = 0;
        //    }
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
