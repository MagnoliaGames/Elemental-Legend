using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbejasMovement : MonoBehaviour
{
    public float movementSpeed;


    public void Move(GameObject target)
    {
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.transform.position, movementSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}
