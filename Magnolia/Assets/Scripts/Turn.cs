using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public bool inverse;

    [Range(-360,360)]
    public float degrees = -90;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player"  && other.GetType() == typeof(CapsuleCollider))
        {
            if (inverse)
            {
                other.transform.localEulerAngles = new Vector3(0, other.transform.localEulerAngles.y - degrees, 0);
                inverse = !inverse;
            }
            else
            {
                other.transform.localEulerAngles = new Vector3(0, other.transform.localEulerAngles.y + degrees, 0);
                inverse = !inverse;
            }
        }
    }
}