using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int puntuacion = 0;
    public string puntuacionString = "Puntuación = ";
    public Text textScore, textVidas;
    public static LevelManager levelManager;

    private GameObject player;

    private void Awake()
    {
        levelManager = this;      
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        if (textVidas != null)
        {
            textVidas.text = "x" + player.GetComponent<PlayerHealth>().vidas.ToString();
        }

        if (textScore != null)
        {
            textScore.text = puntuacionString + puntuacion.ToString();
        }

        if (player.GetComponent<PlayerHealth>().muerto)
        {
            SceneManager.LoadScene("Level 1");
            puntuacion = 0;
        }
    }
}
