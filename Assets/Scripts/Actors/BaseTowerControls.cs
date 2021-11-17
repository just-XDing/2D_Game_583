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

        InvokeRepeating("updateDuccCoin", 1.0f, (0.2f * (float)(player.getDifficulty())));
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
        userTower.duccCoin -= 10;
        userTower.instantiate(0);
    }

    void OnClickLevelsMenu()
    {
        SceneManager.LoadScene("LevelsMenu", LoadSceneMode.Single);
    }

    void Update()
    {
        S_HealthBar.value = userTower.health;
        ButtonToggle();
    }

    void ButtonToggle()
    {
        if (!(BaseTower.roundEnded) && userTower.duccCoin >= 10)
        {
            B_Tier1Summon.interactable = true;
        }
        else
        {
            B_Tier1Summon.interactable = false;
        }
    }

    private void updateDuccCoin()
    {
        if (!(BaseTower.roundEnded) && (userTower.duccCoin < userTower.maxDuccCoin))
        {
            userTower.duccCoin++;
            DuccCoinDisplay.text = (userTower.duccCoin).ToString();
        }
    }
}
