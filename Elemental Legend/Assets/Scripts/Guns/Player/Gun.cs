using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public Transform shot, ikRight, ikLeft;
    public bool canShoot;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
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
}
