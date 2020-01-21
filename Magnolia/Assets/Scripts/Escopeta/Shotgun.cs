using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameObject target;
    private int cont = 0;

    public Transform shot;
    public GameObject bullet;
    public GameObject mira;

    // Start is called before the first frame update
    void Start()
    {
        target = Instantiate(mira);
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.localPosition.x));
        transform.LookAt(target.transform.position);

        if (playerMovement.turned == true && cont == 0)
        {
            transform.localScale = new Vector3(transform.localScale.x *-1, transform.localScale.y, transform.localScale.z * -1);
            cont = 1;
        }
        if (playerMovement.turned == false && cont == 1)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z * -1);
            cont =  0;
        }


        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        
    }

    void Shoot()
    {
        GameObject b1 = Instantiate(bullet, shot.position, transform.rotation);
    }
}
