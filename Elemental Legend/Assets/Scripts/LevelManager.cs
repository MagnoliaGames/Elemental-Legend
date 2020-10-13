using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static int puntuacion = 0;
    public string puntuacionString = "Puntuación = ";

    public Text textScore;

    public static LevelManager levelManager;

    private void Awake()
    {
        levelManager = this;
    }

    void Update()
    {
        if (textScore != null)
        {
            textScore.text = puntuacionString + puntuacion.ToString();
        }
    }
}
