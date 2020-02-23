using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject screenFlash;

   public void Play()
    {
        Invoke("PlayActive", 0.02f);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayActive()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}


