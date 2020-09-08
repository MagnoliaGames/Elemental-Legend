using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform ikRight, ikLeft;
    public bool turned;

    private GameObject player;
    private Animator animator;
    private EnemyHealth health;
    private EnemyVision vision;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");        


        health = GetComponentInParent<EnemyHealth>();
        vision = GetComponentInParent<EnemyVision>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativeEnemy = transform.InverseTransformPoint(player.transform.position);
        if (relativeEnemy.z < 0.0f && turned)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 180, transform.localEulerAngles.z);
            turned = false;
        }
        else if (relativeEnemy.z < 0.0f && !turned)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 180, transform.localEulerAngles.z);
            turned = true;
        }
    }

    private void OnAnimatorIK()
    {
        if (health.muerto)
        {            
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetLookAtWeight(0);
        }
        else
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
            animator.SetLookAtPosition(player.transform.position + new Vector3(0.0f, 1.0f, 0.0f));
            //if (vision.detected)
            //{
                
            //}
        }      
    }
}
