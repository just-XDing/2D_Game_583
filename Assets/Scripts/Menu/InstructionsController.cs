using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionsController : MonoBehaviour
{
    public Button B_Back;
    // Start is called before the first frame update
    void Awake()
    {
        initializeControls();
    }

    void initializeControls()
    {
        B_Back = GameObject.Find("BackButton").GetComponent<Button>();
        B_Back.onClick.AddListener(PressedBack);
    }

    void PressedBack()
    {
        SceneManager.LoadScene("LevelsMenu", LoadSceneMode.Single);
    }
}
