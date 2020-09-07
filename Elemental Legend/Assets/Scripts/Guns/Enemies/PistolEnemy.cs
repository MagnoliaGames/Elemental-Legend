using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolEnemy : GunEnemy
{
    private float time = 0.1f;

    public GameObject bullet;
    void FixedUpdate()
    {
        Look();
        time += 0.02f;
        //if ()
        //{
        //    time = 0;
        //    Shoot();
        //}
    }

    void Shoot()
    {
        Instantiate(bullet, shot.position, shot.rotation);
    }
}
