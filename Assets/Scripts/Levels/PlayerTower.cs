using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerTower : MonoBehaviour
{
    private HealthBase Health;

    // Start is called before the first frame update
    void Start()
    {
        Health = new HealthBase(1000);
    }

    public void takeHealth(int h)
    {
        Health.takeHealth(h);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health.getHealth() <= 0)
        {
            Destroy(this);
        }
    }
}
