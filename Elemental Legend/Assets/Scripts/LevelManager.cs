using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int puntuacion = 0;
    public Text textScore, textVidas, textAmmo, textGranadas;
    public GameObject inGame, pause;
    public static LevelManager levelManager;

    private bool gameRunning;
    private GameObject player;
    private Gun gun;

    private void Awake()
    {
        levelManager = this;
        gameRunning = true;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gun = player.GetComponentInChildren<Gun>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeGameRunningState();
        }

        if (gameRunning)
        {
            inGame.SetActive(true);
            pause.SetActive(false);

            if (textVidas != null)
            {
                textVidas.text = "x" + player.GetComponent<PlayerHealth>().vidas.ToString();
            }

            if (textScore != null)
            {
                textScore.text = "Puntuación = " + puntuacion.ToString();
            }

            if (player.GetComponent<PlayerHealth>().vidas > 0)
            {
                if (gun == null)
                {
                    gun = player.GetComponentInChildren<Gun>();
                }

                if (textAmmo != null && gun != null)
                {
                    if (gun.ammo == 0)
                    {
                        textAmmo.fontSize = 95;
                        textAmmo.text = "∞";
                    }
                    else
                    {
                        textAmmo.fontSize = 50;
                        textAmmo.text = "x" + gun.ammo.ToString();
                    }
                }

                if (textGranadas != null)
                {
                    textGranadas.text = "x" + player.GetComponent<PlayerMovement>().granades.Count.ToString();
                }
            }

            if (player.GetComponent<PlayerHealth>().muerto)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                puntuacion = 0;
            }
        }
        else
        {
            inGame.SetActive(false);
            pause.SetActive(true);
        }
    }

    public void ChangeGameRunningState()
    {
        gameRunning = !gameRunning;

        if (gameRunning)
        {
            Time.timeScale = 1;            
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
