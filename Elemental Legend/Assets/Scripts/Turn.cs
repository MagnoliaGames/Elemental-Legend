﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    private GameObject mCamera;

    public bool inverse, inverseGolem;

    [Range(-360,360)]
    public float degrees = -90;

    private void Start()
    {
        mCamera = GameObject.Find("Main Camera");        
             
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerTurns")  && other.GetType() == typeof(CapsuleCollider))
        {
            StartCoroutine(NormalCamera());
            var playerMovement = other.GetComponentInChildren<PlayerMovement>();
            if (inverse && playerMovement.frente )
            {               
                GameObject.Find("Erick Parent").transform.localEulerAngles = new Vector3(0, GameObject.Find("Erick Parent").transform.localEulerAngles.y + degrees, 0);
            }
            else if(!inverse && !playerMovement.frente)
            {             
                GameObject.Find("Erick Parent").transform.localEulerAngles = new Vector3(0, GameObject.Find("Erick Parent").transform.localEulerAngles.y - degrees, 0);
            }
            else if(!inverse && playerMovement.frente)
            {               
                GameObject.Find("Erick Parent").transform.localEulerAngles = new Vector3(0, GameObject.Find("Erick Parent").transform.localEulerAngles.y + degrees, 0);
            }
            else if (inverse && !playerMovement.frente)
            {               
                GameObject.Find("Erick Parent").transform.localEulerAngles = new Vector3(0, GameObject.Find("Erick Parent").transform.localEulerAngles.y - degrees, 0);
            }
            inverse = !inverse;
        }
        if (other.CompareTag("Enemy") && other.GetComponentInChildren<GolemMovement>())
        {
            if (inverseGolem)
            {
                other.transform.localEulerAngles = new Vector3(0, other.transform.localEulerAngles.y + degrees, 0);
            }        
            else if (!inverseGolem)
            {
                other.transform.localEulerAngles = new Vector3(0, other.transform.localEulerAngles.y - degrees, 0);
            }
            inverseGolem = !inverseGolem;
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