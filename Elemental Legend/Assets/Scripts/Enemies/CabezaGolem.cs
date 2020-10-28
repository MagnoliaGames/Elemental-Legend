using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabezaGolem : MonoBehaviour
{
    public float movementSpeed;

    private GameObject anton;

    private void Start()
    {
        anton = GameObject.Find("General Anton");       
    }

    private void FixedUpdate()
    {
        if (anton != null)
        {
            Move(anton);
        }       
    }

    public void Move(GameObject target)
    {
        Vector3 move = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed);
        transform.position = move;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == anton)
        {
            anton.GetComponent<GeneralAnton>().vidas -= 1;
            Destroy(this.gameObject);
        }
    }
}
