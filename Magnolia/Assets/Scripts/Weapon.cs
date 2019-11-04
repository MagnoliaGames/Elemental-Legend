using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 10;
    public int Damage
    {
        get => damage;
        set => damage = Damage;
    }
}
