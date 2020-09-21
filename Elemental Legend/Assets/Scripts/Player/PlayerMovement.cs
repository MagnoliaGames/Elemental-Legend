using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject erickParent;
    private Rigidbody m_Rigidbody;
    private Vector3 gravity;
    private CameraFollow cameraFollow;
    private Animator animator;
    private Gun gun;
    public bool CanThrow;

    public bool IsGrounded { get; private set; }

    public bool turned, frente;
    public float gravityForce = -9.81f;
    public float movementSpeed = 10;
    public float jumpForce = 15;
    public Transform mouse, ikRight, ikLeft;
    public List<GameObject> granades;

    void Start()
    {
        erickParent = GameObject.Find("Erick Parent");
        m_Rigidbody = GetComponent<Rigidbody>();
        gravity = new Vector3(0, gravityForce, 0);
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        animator = GetComponent<Animator>();
        IsGrounded = true;
        CanThrow = true;
    }

    void FixedUpdate()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<Collider>())
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), enemy.GetComponent<Collider>());
            }       
        }       

        animator.SetBool("walk", false);    

        mouse.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraFollow.generalOffset.x));
        m_Rigidbody.AddForce(gravity, ForceMode.Acceleration);

        if (Input.GetKey(KeyCode.D) && !animator.GetCurrentAnimatorStateInfo(1).IsName("Granade"))
        {
            animator.SetBool("walk", true);            
            frente = true;
            if (turned)
            {
                erickParent.transform.position -= transform.forward * movementSpeed * Time.fixedDeltaTime;
                animator.SetFloat("front", -1);
            }
            else
            {
                erickParent.transform.position += transform.forward * movementSpeed * Time.fixedDeltaTime;
                animator.SetFloat("front", 1);
            }           
        }
        if (Input.GetKey(KeyCode.A) && !animator.GetCurrentAnimatorStateInfo(1).IsName("Granade"))
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
                erickParent.transform.position -= transform.forward * movementSpeed * Time.fixedDeltaTime;
                animator.SetFloat("front", -1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {         
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.G) && 
            granades.Count > 0 && 
            !animator.GetCurrentAnimatorStateInfo(1).IsName("Granade"))
        {           
            animator.SetBool("granade", true);
           
            if (CanThrow)
            {
                StartCoroutine(ThrowGranade());
                
                CanThrow = false;

                StartCoroutine(ReloadGranade());
            }                    
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

        if (gun == null)
        {
            gun = GetComponentInChildren<Gun>();
        }
        gun.gameObject.SetActive(true);

        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Granade"))
        {
            gun.gameObject.SetActive(false);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetLookAtWeight(0);
        }
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
            m_Rigidbody.velocity = Vector3.up * jumpForce;
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
    }

    IEnumerator ThrowGranade()
    {
        yield return new WaitForSeconds(0.8f);
        GameObject granade = granades[granades.Count - 1];
        granades.RemoveAt(granades.Count - 1);
        Instantiate(granade, ikRight.position, new Quaternion());
    }

    IEnumerator ReloadGranade()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("granade", false);
        CanThrow = true;
    }
}
