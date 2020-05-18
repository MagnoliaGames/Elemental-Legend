using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
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
        float angle1 = Random.Range(0, 180);
        float angle2 = Random.Range(0, 180);
        float angle3 = Random.Range(0, 180);
        float angle4 = Random.Range(0, 180);

        Vector3 dir1 = CalculateVector(angle1);
        Vector3 dir2 = CalculateVector(angle2);
        Vector3 dir3 = CalculateVector(angle3);
        Vector3 dir4 = CalculateVector(angle4);

        GameObject b1 = Instantiate(bullet, shot.position, transform.rotation);
        b1.GetComponent<Bullet>().direction = dir1;
        GameObject b2 = Instantiate(bullet, shot.position, transform.rotation);
        b2.GetComponent<Bullet>().direction = dir2;
        GameObject b3 = Instantiate(bullet, shot.position, transform.rotation);
        b3.GetComponent<Bullet>().direction = dir3;
        GameObject b4 = Instantiate(bullet, shot.position, transform.rotation);
        b4.GetComponent<Bullet>().direction = dir4;
    }

    Vector3 CalculateVector(float angle)
    {
        //var rad = Mathf.Sin(angle * Mathf.PI) / 100;
        float y1 = Mathf.Sin(angle);
        float x1 = Mathf.Sqrt(1 - Mathf.Pow(y1, 2));

        return new Vector3(x1, y1);
    }
}
