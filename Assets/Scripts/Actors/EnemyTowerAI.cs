using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerAI : MonoBehaviour
{
    public BaseTower enemyTower;

    // Start is called before the first frame update
    void Start()
    {
       InvokeRepeating("SpawnEnemy", 1.0f, 10.0f);
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
