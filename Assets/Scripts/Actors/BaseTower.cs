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
    public GameObject explosion;
    public static bool roundEnded;

    // Start is called before the first frame update
    void Start()
    {
        roundEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!roundEnded && health <= 0)
        {
            roundEnded = true;
            explosion.transform.localScale = new Vector3(8, 8, 1);
            Instantiate(explosion, this.gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void instantiate(int id)
    {
        if (!roundEnded)
        {
            if (side == CurrentSide.Human)
            {
                Instantiate(availableUnits[id], new Vector3(-9.5f, -2.7f, 0), Quaternion.identity);
                duccCoin -= availableUnits[id].price;
            }
            else
            {
                Instantiate(availableUnits[id], new Vector3(9.5f, -2.7f, 0), Quaternion.identity);
            }
        }
    }
}
