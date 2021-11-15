using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerAI : MonoBehaviour
{
    public BaseTower enemyTower;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
        //InvokeRepeating("SpawnEnemy", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        enemyTower.instantiate(0);
    }
}
