using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    private bool hasCoroutineStarted;
    private GameObject mCamera;

    public bool inverse;

    [Range(-360,360)]
    public float degrees = -90;

    private void Start()
    {
        mCamera = GameObject.Find("Main Camera");
    }

    private void FixedUpdate()
    {
        if (hasCoroutineStarted)
        {
            StartCoroutine(NormalCamera());
            hasCoroutineStarted = false;
        }
        else
        {
            mCamera.transform.SetParent(GameObject.Find("CameraTarget").transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player"  && other.GetType() == typeof(CapsuleCollider) && !hasCoroutineStarted)
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
            hasCoroutineStarted = true;
        }
    }

    IEnumerator NormalCamera()
    {
        mCamera.transform.SetParent(null);      
        yield return new WaitForSeconds(1f);
    }
   
}