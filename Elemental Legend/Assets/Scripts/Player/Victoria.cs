using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victoria : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private Gun gun;

    private void Start()
    {
        playerMovement = GetComponentInChildren<PlayerMovement>();       
    }

    private void Update()
    {
        if (gun == null)
        {
            gun = GetComponentInChildren<Gun>();
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cetro"))
        {
            Destroy(other.GetComponent<Collider>());
            other.transform.position = playerMovement.ikRight.position + new Vector3(0.1f, 0, 0.1f);
            other.transform.SetParent(GameObject.Find("QuickRigCharacter2_RightHand").transform, true);
            Destroy(gun.gameObject);
            playerMovement.transform.localEulerAngles = new Vector3(0, 90, 0);
            playerMovement.victoria = true;
        }
        if (other.CompareTag("Victoria"))
        {
            Destroy(gun.gameObject);
            playerMovement.transform.localEulerAngles = new Vector3(0, 90, 0);
            playerMovement.victoria = true;
        }
    }
}
