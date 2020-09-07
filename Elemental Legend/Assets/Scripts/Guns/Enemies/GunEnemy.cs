using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : MonoBehaviour
{
    private EnemyMovement enemy;
    private GameObject player;
    private EnemyVision vision;

    public Transform shot;

    // Start is called before the first frame update
    void Start()
    {
        vision = GetComponentInParent<EnemyVision>();
        enemy = GetComponentInParent<EnemyMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Look()
    {
        if (vision.detected)
        {
            transform.LookAt(player.transform.position + new Vector3(0, +1.5f, 0));
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
        }      
    }
}
