using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    public CurrentSide side;
    public int health;
    public int maxHealth;
    public int duccCoin;
    public int maxDuccCoin;
    public BasicUnit[] availableUnits;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void instantiate(int id)
    {
        if (side == CurrentSide.Human)
        {
            Instantiate(availableUnits[id], new Vector3(-8.5f, -2.8f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(availableUnits[id], new Vector3(8.5f, -2.8f, 0), Quaternion.identity);
        }
    }
}
