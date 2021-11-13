using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerTower : MonoBehaviour
{
    private HealthBase Health;
    public Button B_Tier1Summon;

    // Start is called before the first frame update
    void Start()
    {
        Health = new HealthBase(1000);
        setupUserControls();
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

    void setupUserControls()
    {
        B_Tier1Summon = GameObject.Find("B_Tier1").GetComponent<Button>();
        B_Tier1Summon.onClick.AddListener(OnClickSummonTier1);
    }


    void OnClickSummonTier1()
    {
    }
}
