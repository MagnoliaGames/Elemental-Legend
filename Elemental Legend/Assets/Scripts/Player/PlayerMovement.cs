using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject erickChild, erickParent;
    private Rigidbody m_Rigidbody;
    private Vector3 gravity;
    private CameraFollow cameraFollow;
    private Animator animator;
    private Gun gun;
    private int jumps;

    public bool IsGrounded { get; private set; }

    public bool turned, frente;
    public bool doubleJump;
    public float gravityForce = -9.81f;
    public float movementSpeed = 10;
    public float jumpForce = 15;
    public Transform mouse, ikRight, ikLeft;

    void Start()
    {
        ikRight = GameObject.Find("IkRight").transform;
        ikLeft = GameObject.Find("IkLeft").transform;
        erickParent = GameObject.Find("Erick Parent");
        erickChild = GameObject.Find("Erick Child");
        m_Rigidbody = GetComponent<Rigidbody>();
        gravity = new Vector3(0, gravityForce, 0);
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        animator = GetComponent<Animator>();
        IsGrounded = true;
    }

    void FixedUpdate()
    {
        animator.SetBool("walk", false);
        if (gun == null)
        {
            gun = GetComponentInChildren<Gun>();
        }

        mouse.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraFollow.generalOffset.x));
        m_Rigidbody.AddForce(gravity, ForceMode.Acceleration);

        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("walk", true);            
            frente = true;
            if (turned)
            {
                erickParent.transform.position += -transform.forward * movementSpeed * Time.fixedDeltaTime;
                animator.SetFloat("front", -1);
            }
            else
            {
                erickParent.transform.position += transform.forward * movementSpeed * Time.fixedDeltaTime;
                animator.SetFloat("front", 1);
            }           
        }
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("walk", true);
            frente = false;
            if (turned)
            {
                erickParent.transform.position += transform.forward * movementSpeed * Time.fixedDeltaTime;
                animator.SetFloat("front", 1);
            }
            else
            {
                erickParent.transform.position += -transform.forward * movementSpeed * Time.fixedDeltaTime;
                animator.SetFloat("front", -1);
            }
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
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);

        animator.SetIKPosition(AvatarIKGoal.RightHand, ikRight.position);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, ikLeft.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, ikRight.rotation);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, ikLeft.rotation);

        animator.SetLookAtWeight(1);
        animator.SetLookAtPosition(mouse.position);
    }

    private void LookMouse()
    {
        if (mouse.localPosition.z < 0 && !turned)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 180, transform.localEulerAngles.z);
            turned = true;
        }
        else if (mouse.localPosition.z < 0 && turned)
        {
            turned = false;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 180, transform.localEulerAngles.z);
        }
    }

    private void Jump()
    {
        animator.SetBool("jump", true);
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
            animator.SetBool("jump", false);
            IsGrounded = true;
        }    
    }

    public void FindIK(Transform Right, Transform Left)
    {
        ikRight = Right;
        ikLeft = Left;
        Debug.Log("hey");
    }
}
