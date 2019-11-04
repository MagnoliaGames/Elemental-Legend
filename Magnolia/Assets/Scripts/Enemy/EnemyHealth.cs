using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private GameObject weapon;
    private Weapon weaponScript;
    private string weaponName;

    public int Health { get; private set; }

    public int initialHealth = 100;

    void Start()
    {
        Health = initialHealth;

        var weapons = GameObject.FindGameObjectsWithTag("Weapon");
        foreach (var weapon in weapons)
        {
            if (weapon.transform.parent.CompareTag("Player"))
            {
                weaponName = weapon.name;
            }
        }
        weapon = GameObject.FindGameObjectWithTag("Player").transform.Find(weaponName).gameObject;
        weaponScript = weapon.GetComponent<Weapon>();

        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerAttack.isAttacking() && other == weapon.GetComponent<BoxCollider>())
        {
            Health -= weaponScript.Damage;
            Debug.Log(Health);
            if (Health <= 0)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
