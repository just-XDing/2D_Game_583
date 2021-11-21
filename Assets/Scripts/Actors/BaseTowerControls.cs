using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BaseTowerControls : MonoBehaviour
{
    public Image HBarColor;
    public TextMeshProUGUI TMP_Victory;
    public SpriteRenderer towerColor;
    public BaseTower userTower;
    public Button[] B_SummonList;
    public Button B_LevelsButton;

    public Slider S_HealthBar;
    public TextMeshProUGUI DuccCoinDisplay;
    private bool canPress;

    // Start is called before the first frame update
    void Start()
    {
        canPress = true;
        InitializePlayer();
        SetupDisplays();
        setupUserControls();
        InvokeRepeating("updateDuccCoin", 1.0f, (0.2f * (int)((Player.Instance).getDifficulty())));
    }

    void InitializePlayer()
    {
        if (Player.Instance != null)
        {
            towerColor.color = (Player.Instance).getColor();
            HBarColor.color = (Player.Instance).getColor();
        }
        else
        {
            gameObject.AddComponent<Player>();
        }
    }

    void SetupDisplays()
    {
        S_HealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        DuccCoinDisplay = GameObject.Find("DuccCoinAmount").GetComponent<TextMeshProUGUI>();
        TMP_Victory = GameObject.Find("Win-LoseText").GetComponent<TextMeshProUGUI>();
    }

    void setupUserControls()
    {
        for (int i = 0; i < B_SummonList.Length; i++)
        {
            B_SummonList[i].interactable = false;
        }
        B_LevelsButton = GameObject.Find("B_Levels").GetComponent<Button>();

        B_SummonList[0].onClick.AddListener(OnClickSummonTier1);
        B_SummonList[1].onClick.AddListener(OnClickSummonTier2);
        B_SummonList[2].onClick.AddListener(OnClickSummonTier3);
        B_SummonList[3].onClick.AddListener(OnClickSummonTier4);
        B_LevelsButton.onClick.AddListener(OnClickLevelsMenu);
    }

    void OnClickSummonTier1()
    {
        userTower.instantiate(0);
        StartCoroutine(buttonCooldown());
    }

    void OnClickSummonTier2()
    {
        userTower.instantiate(1);
        StartCoroutine(buttonCooldown());
    }

    void OnClickSummonTier3()
    {
        userTower.instantiate(2);
        StartCoroutine(buttonCooldown());
    }

    void OnClickSummonTier4()
    {
        userTower.instantiate(3);
        StartCoroutine(buttonCooldown());
    }

    void OnClickLevelsMenu()
    {
        SceneManager.LoadScene("LevelsMenu", LoadSceneMode.Single);
    }

    void Update()
    { 
        if (BaseTower.roundEnded)
        {
            if (userTower.health <= 0)
            {
                TMP_Victory.text = "YOU LOSE\nGo back to the levels by clicking on the back button above";
            }
            else
            {
                TMP_Victory.text = "YOU WIN\nGo back to the levels by clicking on the back button above";
            }
        }
        S_HealthBar.value = userTower.health;
    
        for (int j = (SceneManager.GetActiveScene().buildIndex - 3); j > 0; j--)
        {
            ButtonToggle(j - 1);
        }
    }

    void ButtonToggle(int id)
    {
        if (canPress && !(BaseTower.roundEnded) && userTower.duccCoin >= userTower.availableUnits[id].price)
        {
            B_SummonList[id].interactable = true;
        }
        else
        {
            B_SummonList[id].interactable = false;
        }
    }

    IEnumerator buttonCooldown()
    {
        canPress = false;
        yield return new WaitForSeconds(1);
        canPress = true;
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
