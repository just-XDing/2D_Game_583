using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    Button B_CharGen,
        B_Levels,
        B_Credits,
        EXIT;

    // Start is called before the first frame update
    void Awake()
    {
        initializeControls();
    }

    void initializeControls()
    {
        B_CharGen = GameObject.Find("CharacterGenerator").GetComponent<Button>();
        B_Levels = GameObject.Find("LevelButton").GetComponent<Button>();
        B_Credits = GameObject.Find("CharacterGenerator").GetComponent<Button>();
        EXIT = GameObject.Find("EXITButton").GetComponent<Button>();


        B_CharGen.onClick.AddListener(switchCharGen);
        B_Levels.onClick.AddListener(switchLevels);
        B_Credits.onClick.AddListener(switchCredits);
        EXIT.onClick.AddListener(ExitGame);
    }

    void switchCharGen()
    {
        SceneManager.LoadScene("CharacterGeneration", LoadSceneMode.Single);
    }

    void switchLevels()
    {
        SceneManager.LoadScene("LevelsMenu", LoadSceneMode.Single);
    }

    void switchCredits()
    {
        SceneManager.LoadScene("CreditsMenu", LoadSceneMode.Single);
    }

    void ExitGame()
    {
        //Check if you are in the unity editor. If you are, close in a different way
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        //set playmode to stop
#else
        //otherwise, close the game normally
        Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
