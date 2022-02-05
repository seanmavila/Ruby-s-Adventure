using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    public GameObject soundManager;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        
    }

    public void RestartGame()
    {
        if (SoundManager.instance == null)
        {
            Instantiate(soundManager);
        }
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
