using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameOver = false;
    bool startBattle = false;

    public float respawnTimer;

    public void EndGame()
    {
        if (gameOver == false)
        {
            gameOver = true;
            Invoke("Restart", respawnTimer);
        }
        
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextScene()
    {
        if (startBattle == false)
        {
            startBattle = true;
            Invoke("SceneSwap", 1f);
        }
    }
        
    void SceneSwap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
   
}

