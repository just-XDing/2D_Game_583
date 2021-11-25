using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//keep track of what side a unit is on
public enum CurrentSide : int { Human = -1, Duck = 1};

public class BasicUnit : MonoBehaviour
{
    public int health;
    public int price;
    public CurrentSide side;
}
