using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int valor;
    private int valorX;

    private void Start()
    {
        valorX = valor; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(valorX);
            int dbMoney = DataBase.GetDinero(1);
            dbMoney += valorX;
            DataBase.SetDinero(1, dbMoney);
            GameObject.Destroy(this.gameObject);
        }
    }
}
