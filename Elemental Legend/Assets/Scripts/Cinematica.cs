using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Cinematica : MonoBehaviour
{
    public RawImage video;
    public VideoPlayer movie;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (movie.frame > 0 && movie.isPlaying == false))
        {
            SceneManager.LoadScene(2);
        }
    }
}
