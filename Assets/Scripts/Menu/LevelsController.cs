using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour
{
    Button B_Back;
    Button B_Instructions;
    public Button[] B_Levels;
    // Start is called before the first frame update
    void Start()
    {
        initializeControls();
    }

    void initializeControls()
    {
        B_Back = GameObject.Find("BackButton").GetComponent<Button>();
        B_Instructions = GameObject.Find("Instructions").GetComponent<Button>();
        checkForCompletion();

        B_Back.onClick.AddListener(PressedBack);
        B_Instructions.onClick.AddListener(PressedInstructions);
        //all of these buttons exist in a list. this list
        //is already attached to this script.
        //Initializing them in this function causes errors
        B_Levels[0].onClick.AddListener(OpenLevel11);
        B_Levels[1].onClick.AddListener(OpenLevel12);
        B_Levels[2].onClick.AddListener(OpenLevel13);
        B_Levels[3].onClick.AddListener(OpenLevel14);
        B_Levels[4].onClick.AddListener(OpenLevel15);
    }


    /*
     * checkForCompletion()
     * 
     * This function serves as a hard lock for the buttons
     * in the levels menu. If the previous level has
     * not been completed, this function will disable the
     * interactability of that button.
     * 
     * TL;DR: this basically locks the next levels unless the
     * player completes the previous one.
     */
    void checkForCompletion()
    {
        for (int i = 1; i <= Player.levelsCompleted.Length; i++)
        {
            if (Player.levelsCompleted[i - 1])
            {
                B_Levels[i].interactable = true;
            }
            else
            {
                B_Levels[i].interactable = false;
            }
        }
    }

    void PressedBack()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    void PressedInstructions()
    {
        SceneManager.LoadScene("InstructionsMenu", LoadSceneMode.Single);
    }

    void OpenLevel11()
    {
        SceneManager.LoadScene("Level11", LoadSceneMode.Single);
    }

    void OpenLevel12()
    {
        SceneManager.LoadScene("Level12", LoadSceneMode.Single);
    }

    void OpenLevel13()
    {
        SceneManager.LoadScene("Level13", LoadSceneMode.Single);
    }

    void OpenLevel14()
    {
        SceneManager.LoadScene("Level14", LoadSceneMode.Single);
    }

    void OpenLevel15()
    {
        SceneManager.LoadScene("Level15", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
