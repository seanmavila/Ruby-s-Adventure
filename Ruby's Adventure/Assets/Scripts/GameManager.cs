using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;
    public int score = 0;
    public static GameManager instance = null;
    public BoardManager boardScript;
    public int playerHealth = 100;
    public int playerAmmo = 10;
    public int playerScore;
    public int level;
    public bool gameOver;

    private List<EnemyController> enemies;
    private bool doingSetup;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        enemies = new List<EnemyController>();
        boardScript = GetComponent<BoardManager>();
    }

    private void OnLevelWasLoaded(int index)
    {
        InitGame();
        level++;
    }

    void InitGame()
    {
        doingSetup = true;
        enemies.Clear();
        boardScript.SetupScene(level);
    }

    public void GameOver()
    {
        gameOver = true;
        SoundManager.instance.KillSound();
        Destroy(gameObject);
        SceneManager.LoadScene(3);
    }

}
