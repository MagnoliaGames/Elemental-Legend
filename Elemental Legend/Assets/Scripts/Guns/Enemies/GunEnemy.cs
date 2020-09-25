using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : MonoBehaviour
{
    private GameObject player;

    public Transform shot;
    public EnemyMovement enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<EnemyMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Look()
    {
        if (enemy.warningState)
        {
            transform.LookAt(player.transform.position + new Vector3(0, +1.5f, 0));
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
        }      
    }
}
