using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CGenMenu : MonoBehaviour
{
    public Player play;
    public Button B_Back;

    public Slider S_RedSlider,
        S_GreenSlider,
        S_BlueSlider;

    public TextMeshProUGUI TMP_RedValue,
        TMP_GreenValue,
        TMP_BlueValue;

    public TMP_InputField IF_NameBox;
    public TMP_Dropdown DD_Difficulty;

    public Image preview;
    // Start is called before the first frame update
    void Awake()
    {
        initializeControls();
        initializePlayer();

    }

    void initializeControls()
    {
        B_Back = GameObject.Find("BackButton").GetComponent<Button>();

        S_RedSlider = GameObject.Find("RedSlider").GetComponent<Slider>();
        S_GreenSlider = GameObject.Find("GreenSlider").GetComponent<Slider>();
        S_BlueSlider = GameObject.Find("BlueSlider").GetComponent<Slider>();

        TMP_RedValue = GameObject.Find("RedValue").GetComponent<TextMeshProUGUI>();
        TMP_GreenValue = GameObject.Find("GreenValue").GetComponent<TextMeshProUGUI>();
        TMP_BlueValue = GameObject.Find("BlueValue").GetComponent<TextMeshProUGUI>();

        IF_NameBox = GameObject.Find("NameBox").GetComponent<TMP_InputField>();
        DD_Difficulty = GameObject.Find("DifficultyDropDown").GetComponent<TMP_Dropdown>();

        preview = GameObject.Find("PreviewTower").GetComponent<Image>();
        
        
        B_Back.onClick.AddListener(OnClickBackButton);

        S_RedSlider.onValueChanged.AddListener(delegate { OnChangedRed(); });
        S_GreenSlider.onValueChanged.AddListener(delegate { OnChangedGreen(); });
        S_BlueSlider.onValueChanged.AddListener(delegate { OnChangedBlue(); });
    }

    void OnClickBackButton()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    void OnChangedRed()
    {
        TMP_RedValue.text = S_RedSlider.value.ToString("F2");
    }

    void OnChangedGreen()
    {
        TMP_GreenValue.text = S_GreenSlider.value.ToString("F2");
    }

    void OnChangedBlue()
    {
        TMP_BlueValue.text = S_BlueSlider.value.ToString("F2");
    }


    void initializePlayer()
    {
        if (Player.Instance == null)
        {
            play = gameObject.AddComponent<Player>() as Player;
        }

        loadPreviousData();
    }

    public void loadPreviousData()
    {
        play = Player.Instance;
        IF_NameBox.text = play.getName();

        Color P_Color = play.getColor();
        S_RedSlider.value = P_Color.r;
        S_GreenSlider.value = P_Color.g;
        S_BlueSlider.value = P_Color.b;

        TMP_RedValue.text = S_RedSlider.value.ToString("F2");
        TMP_GreenValue.text = S_GreenSlider.value.ToString("F2");
        TMP_BlueValue.text = S_BlueSlider.value.ToString("F2");

        DD_Difficulty.value = (int)(play.skill);
    }

    // Update is called once per frame
    void Update()
    {
        //rather than a push of a button, update your player real time
        getValuesFromGame();
    }

    

    void getValuesFromGame()
    {
        if (IF_NameBox.text == "")
            play.setName("Player");
        else
            play.setName(IF_NameBox.text);

        changeColor(S_RedSlider.value, S_GreenSlider.value, S_BlueSlider.value);
        play.skill = (Difficulty)(DD_Difficulty.value);
    }

    void changeColor(float r, float g, float b)
    {
        play.setColor(r, g, b);
        preview.color = play.getColor();
    }
}
