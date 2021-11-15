using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseTowerControls : MonoBehaviour
{
    public BaseTower userTower;
    public Button B_Tier1Summon;
    public Slider S_HealthSlider;
    public Image healthFill;
    public SpriteRenderer TowerColor;
    // Start is called before the first frame update
    void Start()
    {
        setupUserControls();
    }

    void setupUserControls()
    {
        //this cyan color will change to the player's color instead
        FillPlayerColor(new Color(0, 255, 255));
        S_HealthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        B_Tier1Summon = GameObject.Find("B_Tier1").GetComponent<Button>();
        B_Tier1Summon.onClick.AddListener(OnClickSummonTier1);
    }

    //the player must have a color, or it will default to white
    void FillPlayerColor(Color col)
    {
        healthFill = GameObject.Find("HealthFiller").GetComponent<Image>();

        //this is the player singleton. the color of the player is based off of this
        healthFill.color = col;//Player.Instance.getColor();
        TowerColor.color = col;
    }

    void OnClickSummonTier1()
    {
        userTower.instantiate(0);
    }

    private void Update()
    {
        S_HealthSlider.value = userTower.health;
    }

    IEnumerator updateDuccCoin()
    {
        if (userTower.duccCoin < userTower.maxDuccCoin)
            userTower.duccCoin++;
        yield return new WaitForSeconds(0.5f);
    }
}
