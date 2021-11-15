using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseTowerControls : MonoBehaviour
{
    public Image HBarColor;
    public SpriteRenderer towerColor;
    public BaseTower userTower;
    public Button B_Tier1Summon;
    public Slider S_HealthBar;
    // Start is called before the first frame update
    void Start()
    {
        Init_UserColors(new Color(0, 255, 255));
        setupUserControls();
    }

    void setupUserControls()
    {
        S_HealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        B_Tier1Summon = GameObject.Find("B_Tier1").GetComponent<Button>();
        B_Tier1Summon.onClick.AddListener(OnClickSummonTier1);
    }

    void Init_UserColors(Color col)
    {
        HBarColor.color = col;
        towerColor.color = col;
    }

    void OnClickSummonTier1()
    {
        userTower.instantiate(0);
    }

    private void Update()
    {
        S_HealthBar.value = ((float)userTower.health / userTower.maxHealth) * 100;
    }
}
