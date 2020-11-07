using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GeneralAnton : MonoBehaviour
{
    private Animator animator;
    private GameObject player;

    public int vidas;
    public GameObject cetro, jaula;
    public GameObject[] golems, finalTurns;
    public Transform[] spawns;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.LookAt(player.transform);
        
        GameObject[] enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> golemsInScene = BuscarGolems(enemiesInScene);

        if (golemsInScene.Count <= 0)
        {
            animator.SetBool("Ataque", true);
            Instantiate(golems[0], spawns[Random.Range(0, 2)].position, new Quaternion());
            Instantiate(golems[1], spawns[Random.Range(2, 4)].position, new Quaternion());
            StartCoroutine(ReloadAtaque());
        }  
        if (vidas <= 0)
        {            
            animator.SetBool("Muerte", true);
            if (GetComponentInChildren<Flock>() != null)
            {
                Flock flock = GetComponentInChildren<Flock>();
                flock.destroy = true;
            }            
            StartCoroutine(Destroy());
        }
    }

    List<GameObject> BuscarGolems(GameObject[] enemies)
    {
        List<GameObject> golems = new List<GameObject>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetComponentInChildren<GolemMovement>())
            {
                golems.Add(enemies[i]);
            }
        }
        return golems;
    }

    IEnumerator ReloadAtaque()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("Ataque", false);
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.15f);
        GameObject[] enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> golemsInScene = BuscarGolems(enemiesInScene);
        foreach (GameObject golem in golemsInScene)
        {
            Destroy(golem);
        }               
        Destroy(jaula);
        Instantiate(cetro, new Vector3(0, 2.4f, 0), new Quaternion());
        foreach (GameObject finalTurn in finalTurns)
        {
            if (Vector3.Distance(player.transform.position, finalTurn.transform.position) > 2)
            {
                finalTurn.SetActive(true);
            }
            if (finalTurn.activeSelf)
            {
                Destroy(this.gameObject);
            }
        }        
    }
}
