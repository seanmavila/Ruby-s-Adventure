using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;
    public static GameManager instance = null;
    public BoardManager boardScript;

    private int level = 3;
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
        InitGame();
    }

    private void OnLevelWasLoaded(int inex)
    {
        level++;
        InitGame();
    }

    void InitGame()
    {
        doingSetup = true;
        enemies.Clear();
        boardScript.SetupScene(level);
    }

}
