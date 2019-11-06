using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
