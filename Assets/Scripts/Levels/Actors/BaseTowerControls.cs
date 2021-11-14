using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseTowerControls : MonoBehaviour
{
    public BaseTower userTower;
    public Button B_Tier1Summon;
    // Start is called before the first frame update
    void Start()
    {
        setupUserControls();
    }

    void setupUserControls()
    {
        B_Tier1Summon = GameObject.Find("B_Tier1").GetComponent<Button>();
        B_Tier1Summon.onClick.AddListener(OnClickSummonTier1);
    }

    void OnClickSummonTier1()
    {
        userTower.instantiate(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
