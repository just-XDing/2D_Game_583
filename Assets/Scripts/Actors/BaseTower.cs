using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseTower : MonoBehaviour
{
    public CurrentSide side;
    public int health;
    public int maxHealth;
    public int duccCoin;
    public int maxDuccCoin;
    public BasicUnit[] availableUnits;
    public GameObject explosion;

    //there only needs to be one version of roundEnded.
    public static bool roundEnded;

    // Start is called before the first frame update
    void Start()
    {
        roundEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if the round has ended and any tower dies
        if (!roundEnded && health <= 0)
        {
            //any coroutines that existed must be stopped
            StopAllCoroutines();

            //keeps the update function from calling this over and over
            roundEnded = true;

            //scale an explosion game object and spawn it, while destroying the tower itself.
            explosion.transform.localScale = new Vector3(8, 8, 1);
            Instantiate(explosion, this.gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void instantiate(int id)
    {
        //check if the game has finished and the player's side is a human or not
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
