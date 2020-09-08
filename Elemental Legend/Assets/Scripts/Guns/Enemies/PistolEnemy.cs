using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolEnemy : GunEnemy
{

    public GameObject bullet;
    public float time;
    public bool canShoot = true;

    void FixedUpdate()
    {
        Look();
        if (vision.detected && canShoot)
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

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
}
