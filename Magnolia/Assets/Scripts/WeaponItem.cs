using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public GameObject espada;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            GameObject sword = Instantiate(espada);
            sword.transform.SetParent(other.transform);
            sword.transform.localPosition = new Vector3(0.68f, -0.17f, 0.11f);
            Destroy(this.gameObject);
        }
    }
}
