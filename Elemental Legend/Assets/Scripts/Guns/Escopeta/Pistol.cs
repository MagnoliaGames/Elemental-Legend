using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public GameObject bullet;
    void FixedUpdate()
    {
        Look();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bullet, shot.position,shot.rotation);
    }
}
