using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    public CurrentSide side;
    public int health;
    public int money;
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
            Debug.Log("Instantiated a thingy");
            Instantiate(availableUnits[id], new Vector3(-6.5f, -2.8f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(availableUnits[id], new Vector3(6.5f, -2.8f, 0), Quaternion.identity);
        }
    }
}
