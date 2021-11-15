using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BaseTowerControls : MonoBehaviour
{
    public Image HBarColor;
    public SpriteRenderer towerColor;
    public BaseTower userTower;
    public Button B_Tier1Summon;

    public Slider S_HealthBar;
    public TextMeshProUGUI DuccCoinDisplay;
    // Start is called before the first frame update
    void Start()
    {
        Init_UserColors(new Color(0, 255, 255));
        SetupDisplays();
        setupUserControls();
    }

    void Init_UserColors(Color col)
    {
        HBarColor.color = col;
        towerColor.color = col;
    }

    void SetupDisplays()
    {
        S_HealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        DuccCoinDisplay = GameObject.Find("DuccCoinAmount").GetComponent<TextMeshProUGUI>();
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


    bool moneyCooldown = true;
    float moneyRate = 10.0f;
    private void Update()
    {
        S_HealthBar.value = userTower.health;
        updateDuccCoin();
        
    }

    private void updateDuccCoin()
    {
        if (moneyCooldown)
        {
            StartCoroutine(moneyRateControl());
        }
        else
        {
            if (userTower.duccCoin < userTower.maxDuccCoin)
            {
                userTower.duccCoin++;
                DuccCoinDisplay.text = (userTower.duccCoin).ToString();
            }
        }
    }

    IEnumerator moneyRateControl()
    {
        moneyCooldown = false;
        yield return new WaitForSeconds(moneyRate);
        moneyCooldown = true;
    }
}
