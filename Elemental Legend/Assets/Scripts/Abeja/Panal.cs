using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panal : MonoBehaviour
{
    public Flock Abejas, abejasLiberadas;
    public float rangoInteraccion;

    private GameObject player;
    private bool liberadas;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Erick Parent");
        liberadas = false;
    }

    // Update is called once per frame
    void Update()
    {     
        if (Vector3.Distance(player.transform.position, transform.position) <= rangoInteraccion)
        {            
            if (!liberadas && abejasLiberadas == null)
            {
                abejasLiberadas = GameObject.Instantiate(Abejas, this.transform.position, new Quaternion());
                liberadas = true;
            }
            abejasLiberadas.GetComponent<AbejasMovement>().Move(player);
        }
        else if (Vector3.Distance(player.transform.position, transform.position) > rangoInteraccion)
        {
            if (abejasLiberadas != null)
            {
                abejasLiberadas.GetComponent<AbejasMovement>().Move(this.gameObject);
                if (Vector3.Distance(transform.position, abejasLiberadas.transform.position) <= 0.1f)
                {
                    abejasLiberadas.destroy = true;
                }
            }
            if (liberadas)
            {
                liberadas = false;
            }
        }
    }
}
