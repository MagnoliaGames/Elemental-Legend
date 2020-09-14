using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaSuministro : MonoBehaviour
{
    private GameObject player, erickChild;

    public GameObject[] drops;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        erickChild = GameObject.Find("Erick Child");
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Destroy(erickChild.GetComponentInChildren<Gun>().gameObject);
            GameObject drop = drops[Random.Range(0, drops.Length)];
            GameObject ActualDrop = GameObject.Instantiate(drop.gameObject, erickChild.transform);
            player.GetComponent<PlayerMovement>().FindIK(ActualDrop.GetComponent<Gun>().ikRight, ActualDrop.GetComponent<Gun>().ikLeft);
            Destroy(this.gameObject);      
        }
    }
}
