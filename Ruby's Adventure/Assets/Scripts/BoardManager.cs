using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns;
    public int rows;
    public Count obstacleCount = new Count(5, 9);
    public Count foodCount = new Count(3, 8);
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] obstacleTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] waterTiles;
    public GameObject[] npcTiles;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitializeList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup(int level)
    {
        boardHolder = new GameObject("Board").transform;

        if (level == 0)
        {
            columns += level;
            rows += level;
        }
        else
        {
            columns += level / 2;
            rows += level / 2;
        }
        

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(1, floorTiles.Length)];
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = waterTiles[Random.Range(0, waterTiles.Length)];
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }

    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void  LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup(level);
        InitializeList();
        if (level == 0)
        {
            int npcCount = 1;
            LayoutObjectAtRandom(npcTiles, npcCount, npcCount);
            Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
        }
        else
        {
            LayoutObjectAtRandom(obstacleTiles, obstacleCount.minimum + 3 * level, obstacleCount.maximum + 3 * level);
            LayoutObjectAtRandom(foodTiles, foodCount.minimum + 3 * level, foodCount.maximum + 3 * level);
            int enemyCount = level + 2;
            LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
            Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
        }
        
    }
}
