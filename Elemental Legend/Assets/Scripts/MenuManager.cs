using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject score, mainMenu;
    public Text puntuacionLevel1, puntuacionLevel2, puntuacionLevel3, puntuacionLevel4;

    private void Awake()
    {
        mainMenu.SetActive(true);
        score.SetActive(false);

        puntuacionLevel1.text = DataBase.GetPuntuacion(1).ToString();
        puntuacionLevel2.text = DataBase.GetPuntuacion(2).ToString();
        puntuacionLevel3.text = DataBase.GetPuntuacion(3).ToString();
        puntuacionLevel4.text = DataBase.GetPuntuacion(4).ToString();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Score()
    {
        mainMenu.SetActive(false);
        score.SetActive(true);
    }

    public void BackMenu()
    {
        mainMenu.SetActive(true);
        score.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
