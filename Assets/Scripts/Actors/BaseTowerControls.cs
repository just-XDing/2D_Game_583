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
    public BaseTower enemyTower;
    public Button[] B_SummonList;
    public Button B_LevelsButton;

    public Slider S_HealthBar;
    public Slider S_EnemyHealth;
    public TextMeshProUGUI DuccCoinDisplay;
    private bool canPress;

    // Start is called before the first frame update
    void Start()
    {
        canPress = true;
        InitializePlayer();
        SetupDisplays();
        setupUserControls();
        //repeat calling a function for currency
        InvokeRepeating("updateDuccCoin", 1.0f, (0.15f * (int)((Player.Instance).getDifficulty())));
    }

    //If a player never existed, then make a new one (but this may break many things if you start the game at any level scene)
    //this was only here for testing purposes
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

    //player's health bar, money, and possible victory screen
    void SetupDisplays()
    {
        S_HealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        DuccCoinDisplay = GameObject.Find("DuccCoinAmount").GetComponent<TextMeshProUGUI>();
        TMP_Victory = GameObject.Find("Win-LoseText").GetComponent<TextMeshProUGUI>();
    }

    void setupUserControls()
    {
        //set all of the buttons to false initially
        for (int i = 0; i < B_SummonList.Length; i++)
        {
            B_SummonList[i].interactable = false;
        }
        B_LevelsButton = GameObject.Find("B_Levels").GetComponent<Button>();

        //See LevelsController.cs for the reason why these buttons
        //are not initialized in this funciton.
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
        //if the game ended
        if (BaseTower.roundEnded)
        {
            if (userTower.health <= 0)
            {
                TMP_Victory.text = "YOU LOSE\nGo back to the levels by clicking on the back button above";
            }
            else
            {
                TMP_Victory.text = "YOU WIN\nGo back to the levels by clicking on the back button above";
                //update the completed level list in the player singleton
                //the bonus level should not affect the player's completion array
                if(SceneManager.GetActiveScene().buildIndex != 8)
                    Player.levelsCompleted[SceneManager.GetActiveScene().buildIndex - 4] = true;
            }
        }

        //update the health bars of the enemy and the player
        S_EnemyHealth.value = enemyTower.health;
        S_HealthBar.value = userTower.health;
        

        for (int j = (SceneManager.GetActiveScene().buildIndex - 3); j > 0; j--)
        {
            //index 5 does not exist in the player's availableUnits[]
            if (j == 5)
                continue;
            else
                ButtonToggle(j - 1);
        }
    }

    void ButtonToggle(int id)
    {
        //if the player has sufficient funds to even buy any unit
        if (canPress && !(BaseTower.roundEnded) && userTower.duccCoin >= userTower.availableUnits[id].price)
        {
            B_SummonList[id].interactable = true;
        }
        else
        {
            B_SummonList[id].interactable = false;
        }
    }

    //this is to keep the player from spamming unit buttons like crazy
    IEnumerator buttonCooldown()
    {
        canPress = false;
        yield return new WaitForSeconds(1);
        canPress = true;
    }

    private void updateDuccCoin()
    {
        //keep the user's ducc Coin (money) amount from exceeding the max amount
        if (!(BaseTower.roundEnded) && (userTower.duccCoin < userTower.maxDuccCoin))
        {
            userTower.duccCoin++;
            DuccCoinDisplay.text = (userTower.duccCoin).ToString();
        }
    }
}
