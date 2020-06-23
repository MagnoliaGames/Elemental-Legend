using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    private float time = 0.1f;

    public GameObject bullet;
    void FixedUpdate()
    {
        Look();
        time += 0.02f;
        if (Input.GetMouseButtonDown(0) && time >= 0.1f)
        {
            time = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bullet, shot.position, shot.rotation);
    }
}
