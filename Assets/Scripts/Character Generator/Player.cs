using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public static bool[] levelsCompleted;
    private string playerName;
    private Color color;
    private Difficulty skill;

    // "constructor"
    public void Awake()
    {
        //if there is no instance of a DNDPlayer, make a new one, and load its data.
        //Singleton check was referenced from Slides
        if (Instance == null)
        {
            Instance = this;
            levelsCompleted = new bool[4];
            DontDestroyOnLoad(this);
            loadData();
        }
        //otherwise, destroy the current instance and replace it with a new one.
        else
        {
            Destroy(this);
        }
    }

    public void loadData()
    {
        playerName = "";
        color = new Color(1.0f, 1.0f, 1.0f);
        skill = Difficulty.Easy;
    }

    //setters and getters for instance variables
    public void setName(string n) { playerName = n; }
    public string getName() { return playerName; }

    public void setColor(float r, float g, float b) { color = new Color(r,g,b); }
    public Color getColor() { return color; }

    public void setDifficulty(Difficulty x) { skill = x; }
    public Difficulty getDifficulty() { return skill; }

}
