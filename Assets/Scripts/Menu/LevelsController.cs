using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour
{
    Button B_Back,
        B_Level11,
        B_Level12,
        B_Level13,
        B_Level14,
        B_Level15;
    // Start is called before the first frame update
    void Start()
    {
        initializeControls();
    }

    void initializeControls()
    {
        B_Back = GameObject.Find("BackButton").GetComponent<Button>();
        B_Level11 = GameObject.Find("Level1").GetComponent<Button>();
        B_Level12 = GameObject.Find("Level2").GetComponent<Button>();

        B_Back.onClick.AddListener(PressedBack);
        B_Level11.onClick.AddListener(OpenLevel11);
        B_Level12.onClick.AddListener(OpenLevel12);
    }

    void PressedBack()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    void OpenLevel11()
    {
        SceneManager.LoadScene("Level11", LoadSceneMode.Single);
    }

    void OpenLevel12()
    {
        SceneManager.LoadScene("Level12", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
