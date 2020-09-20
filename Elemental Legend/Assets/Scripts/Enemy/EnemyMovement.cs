using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform ikRight, ikLeft;
    public bool turned, warningState;

    private GameObject player;
    private Animator animator;
    private EnemyHealth health;
    private EnemyVision vision;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");        
        health = GetComponentInParent<EnemyHealth>();
        vision = GetComponentInParent<EnemyVision>();

        warningState = false;
    }

    void FixedUpdate()
    {
        if (vision.detected || 
            Vector3.Distance(player.transform.position, transform.position) <= 1 || 
            health.bulletHit ||
            !health.granadeHitable)
        {
            warningState = true;
        }
        else
        {
            warningState = false;
        }

        if (warningState)
        {
            Vector3 relativePlayer = transform.InverseTransformPoint(player.transform.position);
            if (relativePlayer.z < 0.0f && turned)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 180, transform.localEulerAngles.z);
                turned = false;
            }
            else if (relativePlayer.z < 0.0f && !turned)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 180, transform.localEulerAngles.z);
                turned = true;
            }
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
                       
            if (vision.detected)
            {
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(player.transform.position + new Vector3(0.0f, 1.0f, 0.0f));
            }
        }      
    }
}
