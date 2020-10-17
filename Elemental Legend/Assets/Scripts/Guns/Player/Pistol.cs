using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public GameObject bullet;
    void FixedUpdate()
    {
        Look();
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Shoot();
            canShoot = false;
            StartCoroutine(Reload());           
        }
    }

    void Shoot()
    {
        Instantiate(bullet, shot.position, shot.rotation);
    }
}
