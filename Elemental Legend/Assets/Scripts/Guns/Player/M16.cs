using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M16 : Gun
{
    public GameObject bullet;

    void FixedUpdate()
    {
        Look();
        if (Input.GetMouseButton(0) && canShoot)
        {
            Shoot();
            canShoot = false;
            ammo -= 1;
            StartCoroutine(Reload());
        }
        ChangeWeapon();
    }

    void Shoot()
    {
        Instantiate(bullet, shot.position, shot.rotation);
    }
}
