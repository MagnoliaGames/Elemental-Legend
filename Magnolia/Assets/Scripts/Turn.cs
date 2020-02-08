using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    private GameObject mCamera;

    public bool inverse;

    [Range(-360,360)]
    public float degrees = -90;

    private void Start()
    {
        mCamera = GameObject.Find("Main Camera");        
             
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")  && other.GetType() == typeof(CapsuleCollider))
        {
            StartCoroutine(NormalCamera());
            var playerMovement = other.GetComponent<PlayerMovement>();
            if (inverse && /*other.transform.localScale.x > 0*/ !playerMovement.turned)
            {
                other.transform.localEulerAngles = new Vector3(0, other.transform.localEulerAngles.y + degrees, 0);
            }
            else if(!inverse && /*other.transform.localScale.x < 0*/ playerMovement.turned)
            {
                other.transform.localEulerAngles = new Vector3(0, other.transform.localEulerAngles.y - degrees, 0);
            }
            else if(!inverse && /*other.transform.localScale.x > 0*/ !playerMovement.turned)
            {
                other.transform.localEulerAngles = new Vector3(0, other.transform.localEulerAngles.y + degrees, 0);
            }
            else if (inverse && /*other.transform.localScale.x < 0*/ playerMovement.turned)
            {
                other.transform.localEulerAngles = new Vector3(0, other.transform.localEulerAngles.y - degrees, 0);
            }
            inverse = !inverse;
        }
    }

    IEnumerator NormalCamera()
    {
        mCamera.transform.SetParent(null);
        yield return new WaitForSeconds(0.1f);
        mCamera.transform.SetParent(GameObject.Find("CameraTarget").transform);
        StopCoroutine(NormalCamera());
    }

}