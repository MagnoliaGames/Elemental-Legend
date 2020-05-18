﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float roty;
    public Transform shot;

    // Start is called before the first frame update
    void Start()
    {

        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void Look()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, roty - 3f, transform.localEulerAngles.z);
        transform.LookAt(playerMovement.mouse.transform);
    }
}
