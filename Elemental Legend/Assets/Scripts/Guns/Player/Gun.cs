using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameObject erickChild;

    public Transform shot, ikRight, ikLeft;
    public GameObject pistol;
    public bool canShoot;
    public float time;
    public int ammo;

    void Start()
    {
        canShoot = true;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        erickChild = GameObject.Find("Erick Child");
    }

    public void Look()
    {
        transform.LookAt(playerMovement.mouse);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);      
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }

    public void ChangeWeapon()
    {
        if (ammo <= 0)
        {
            Destroy(erickChild.GetComponentInChildren<Gun>().gameObject);
            GameObject ActualPistol = GameObject.Instantiate(pistol.gameObject, erickChild.transform);
            playerMovement.FindIK(ActualPistol.GetComponent<Gun>().ikRight, ActualPistol.GetComponent<Gun>().ikLeft);
            Destroy(this.gameObject);
        }       
    }
}
