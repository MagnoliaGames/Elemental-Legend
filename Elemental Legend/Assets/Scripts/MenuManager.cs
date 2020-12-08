using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject levels, mainMenu, credits;
    public Text puntuacionLevel1, puntuacionLevel2, puntuacionLevel3, puntuacionLevel4;

    private void Awake()
    {
        mainMenu.SetActive(true);
        levels.SetActive(false);

        puntuacionLevel1.text = DataBase.GetPuntuacion(1).ToString();
        puntuacionLevel2.text = DataBase.GetPuntuacion(2).ToString();
        puntuacionLevel3.text = DataBase.GetPuntuacion(3).ToString();
        puntuacionLevel4.text = DataBase.GetPuntuacion(4).ToString();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void Levels()
    {
        mainMenu.SetActive(false);
        levels.SetActive(true);
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void BackMenu()
    {
        if (levels.activeSelf)
        {
            mainMenu.SetActive(true);
            levels.SetActive(false);
        }
        if (credits.activeSelf)
        {
            mainMenu.SetActive(true);
            credits.SetActive(false);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
