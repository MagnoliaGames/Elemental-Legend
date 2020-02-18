using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameObject target;
    private int cont = 0;
    float roty;

    public Transform shot;
    public GameObject bullet;
    public GameObject mira;

    // Start is called before the first frame update
    void Start()
    {
        target = Instantiate(mira);
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        target.transform.SetParent(playerMovement.transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.localPosition.x));
        transform.LookAt(target.transform);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, roty - 3f, transform.localEulerAngles.z);

        if (playerMovement.turned == false && target.transform.localPosition.z < 0)
        {
            if (cont == 0)
            {
                playerMovement.transform.localScale = new Vector3(-1, 1, -1);
                transform.localScale = new Vector3(3.4f, 3.4f, -3.4f);
                roty = 180;
                cont = 1;
            }
            else if (cont == 1)
            {
                playerMovement.transform.localScale = new Vector3(1, 1, 1);
                transform.localScale = new Vector3(3.4f, 3.4f, 3.4f);
                roty = 0;
                cont = 0;
            }

        }
        else if (playerMovement.turned == true && target.transform.localPosition.z < 0)
        {
            if (cont == 0)
            {
                playerMovement.transform.localScale = new Vector3(1, 1, 1);
                transform.localScale = new Vector3(3.4f, 3.4f, 3.4f);
                roty = 0;
                cont = 1;
            }
            else if (cont == 1)
            {
                playerMovement.transform.localScale = new Vector3(-1, 1, -1);
                transform.localScale = new Vector3(3.4f, 3.4f, -3.4f);
                roty = 180;
                cont = 0;
            }
        }

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
