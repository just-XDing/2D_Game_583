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
    public Button B_LevelsButton;
    public Player player;

    public Slider S_HealthBar;
    public TextMeshProUGUI DuccCoinDisplay;
    // Start is called before the first frame update
    void Start()
    {
        InitializePlayer();
        SetupDisplays();
        setupUserControls();
    }

    void InitializePlayer()
    {
        if (Player.Instance == null)
        {
            player = gameObject.AddComponent<Player>() as Player;
        }

        towerColor.color = player.getColor();
        HBarColor.color = player.getColor();
    }

    void SetupDisplays()
    {
        S_HealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        DuccCoinDisplay = GameObject.Find("DuccCoinAmount").GetComponent<TextMeshProUGUI>();
    }

    void setupUserControls()
    {
        B_Tier1Summon = GameObject.Find("B_Tier1").GetComponent<Button>();
        B_LevelsButton = GameObject.Find("B_Levels").GetComponent<Button>();

        B_Tier1Summon.onClick.AddListener(OnClickSummonTier1);
        B_LevelsButton.onClick.AddListener(OnClickLevelsMenu);
    }

    void OnClickSummonTier1()
    {
        userTower.instantiate(0);
    }

    void OnClickLevelsMenu()
    {
        SceneManager.LoadScene("LevelsMenu", LoadSceneMode.Single);
    }

    void Update()
    {
        S_HealthBar.value = userTower.health;
        updateDuccCoin();

    }

    bool moneyCooldown = true;
    float moneyRate = 10.0f;

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
