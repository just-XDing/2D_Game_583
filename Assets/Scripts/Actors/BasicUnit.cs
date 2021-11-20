using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentSide : int { Human = -1, Duck = 1};

public class BasicUnit : MonoBehaviour
{
    public int health;
    public int price;
    //public SpriteRenderer icon;
    public CurrentSide side;
    // Start is called before the first frame update
    void Start()
    {
        //icon = GetComponent<SpriteRenderer>();
    }
}
