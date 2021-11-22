using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTowerAI : MonoBehaviour
{
    public BaseTower enemyTower;

    // Start is called before the first frame update
    void Start()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 4:
                InvokeRepeating("Level1Spawns", 1.0f, 5.0f);
                break;
            case 5:
                InvokeRepeating("Level1Spawns", 1.0f, 12.0f);
                InvokeRepeating("Level2Spawns", 15.0f, 8.0f);
                break;
            case 6:
                InvokeRepeating("Level1Spawns", 6.0f, 10.0f);
                InvokeRepeating("Level2Spawns", 30.0f, 9.0f);
                InvokeRepeating("Level3Spawns", 20.0f, 8.0f);
                break;
            case 7:
                InvokeRepeating("Level1Spawns", 1.0f, 7.0f);
                InvokeRepeating("Level3Spawns", 40.0f, 20.0f);
                InvokeRepeating("Level4Spawns", 25.0f, 25.0f);
                break;
            case 8:
                InvokeRepeating("Level1Spawns", 1.0f, 7.0f);
                InvokeRepeating("Level3Spawns", 10.0f, 9.0f);
                InvokeRepeating("Level4Spawns", 20.0f, 30.0f);
                break;
        }
    }

    void Level1Spawns()
    {
        enemyTower.instantiate(0);
    }

    void Level2Spawns()
    {
        enemyTower.instantiate(1);
    }

    void Level3Spawns()
    {
        enemyTower.instantiate(2);
    }

    void Level4Spawns()
    {
        enemyTower.instantiate(3);
    }
}
