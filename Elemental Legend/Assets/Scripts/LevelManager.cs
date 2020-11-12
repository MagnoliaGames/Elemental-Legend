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
    private int count = 0;

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
                textVidas.text = "x" + player.GetComponentInParent<PlayerHealth>().vidas.ToString();
            }

            if (textScore != null)
            {
                textScore.text = "Puntuación = " + puntuacion.ToString();
            }

            if (player.GetComponentInParent<PlayerHealth>().vidas > 0)
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

                if (player.GetComponent<PlayerMovement>().victoria)
                {
                    if (count == 0)
                    {
                        puntuacion *= player.GetComponentInParent<PlayerHealth>().vidas;
                        count += 1;
                    }
                    StartCoroutine(LoadScene());
                }
            }

            if (player.GetComponentInParent<PlayerHealth>().muerto)
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

    IEnumerator LoadScene(){        
        yield return new WaitForSeconds(2f);        
        if (SceneManager.GetActiveScene().name == "Level 1")
        {            
            if (DataBase.GetPuntuacion(1) < puntuacion)
            {
                DataBase.SetPuntuacion(1, puntuacion);
            }
            puntuacion = 0;
            SceneManager.LoadScene(2);
        }
        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            if (DataBase.GetPuntuacion(2) < puntuacion)
            {
                DataBase.SetPuntuacion(2, puntuacion);
            }
            puntuacion = 0;
            SceneManager.LoadScene(3);
        }
        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            if (DataBase.GetPuntuacion(3) < puntuacion)
            {
                DataBase.SetPuntuacion(3, puntuacion);
            }
            puntuacion = 0;
            SceneManager.LoadScene(4);
        }
        if (SceneManager.GetActiveScene().name == "Level 4")
        {
            if (DataBase.GetPuntuacion(4) < puntuacion)
            {
                DataBase.SetPuntuacion(4, puntuacion);
            }
            puntuacion = 0;
            SceneManager.LoadScene(0);
        }
        count = 0;
    }
}
