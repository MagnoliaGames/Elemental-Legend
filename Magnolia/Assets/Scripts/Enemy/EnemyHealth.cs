using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    public GameObject[] drops;

    private void Update()
    {
        if (health <= 0)
        {
            GameObject.Instantiate(drops[Random.Range(0,drops.Length)], transform.position, new Quaternion());
            Destroy(this.gameObject);
        }
    }
}
