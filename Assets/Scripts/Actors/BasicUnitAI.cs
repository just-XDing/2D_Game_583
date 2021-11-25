using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//all of the animations and conditions are at least dictated by 
//their own state
public enum States { Idle, Run, Attack, Die}

public class BasicUnitAI : MonoBehaviour
{
    public float speed;
    public int dmg;
    public float range;
    public int scale;
    public Animator anim;
    public LayerMask layer;
    public GameObject explosion;
    public AudioSource Sound_Hit;

    //
    private BasicUnit unit;
    private Vector2 dir;
    private RaycastHit2D rayCast;
    private bool canFight;
    private States state;

    // Start is called before the first frame update
    void Start()
    {
        //at this stage, the game is idle
        Sound_Hit = GetComponent<AudioSource>();
        canFight = true;
        state = States.Idle;
        unit = GetComponent<BasicUnit>();
       
        // make player go left if on the human side; otherwise, if its a duck
        // then go to the right
        dir = unit.side == CurrentSide.Human ? Vector2.right : Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
        //check if the unit has no health at least
        if (unit.health <= 0)
        {
            Die();
        }
        //keep updating the state of the unit and its raycast
        UpdateState();
        //this is used to determine the range of the unit.
        rayCast = Physics2D.Raycast(getRayCastPosition(), dir, range, layer);

        //if the round hasn't ended, the raycast has picked up on something, and that something
        //is an enemy
        if (!(BaseTower.roundEnded) && rayCast.collider != null && oppositeTag(rayCast.collider))
        {
            Fight();
        }
        //otherwise, check for the round not ending
        else if (!(BaseTower.roundEnded))
        {
            //if the unit detects another friendly unit
            if (rayCast.collider != null && !oppositeTag(rayCast.collider))
            {
                Physics2D.IgnoreCollision(rayCast.collider, this.GetComponent<BoxCollider2D>());
            }

            //update state
            state = States.Run;
            
            //do a switch case of the side of the unit to determine what direction and how far to go
            switch (unit.side)
            {
                case CurrentSide.Human:
                    unit.transform.position += Vector3.right * speed;
                    break;
                case CurrentSide.Duck:
                    unit.transform.position += Vector3.left * speed;
                    break;
            }
        }
        else
        {
            //this only happens IF the BaseTower.roundEnded happened
            //basically, if the round has ended.
            state = States.Idle;
            speed = 0;
        }
    }

    //this ignores collisions with things of the same tag.
    //the other thing in the Update() ignores detection in respect to its
    //raycast
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!oppositeTag(collision.collider) && !collision.collider.CompareTag("Untagged"))
        {
            Physics2D.IgnoreCollision(collision.collider, this.GetComponent<BoxCollider2D>());
        }
    }

    void Die()
    {
        //update its state
        canFight = false;
        state = States.Die;
        //stop any coroutines
        StopCoroutine(fightCooldown());
        //blow up and delete itself
        explosion.transform.localScale = new Vector3(scale, scale, 1);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    //returns a Vector3 of where to initialize the raycast
    Vector3 getRayCastPosition()
    {
        //do a raycast depending on the side of the unit
        //this is direction dependent
        switch (unit.side)
        {
            //it turns out that 0.2 is a magical number (as 0.1 does break it)
            case CurrentSide.Human:
                return unit.transform.position + new Vector3(0.2f, 0);
                
            case CurrentSide.Duck:
                return unit.transform.position + new Vector3(-0.2f, 0);
            default:
                return unit.transform.position;
        }
    }

    /*
     * UpdateState()
     * 
     * this basically dictates the animation of the unit.
     * all units have the same "state" controller in their
     * animations.
     */
    void UpdateState()
    {
        switch(state)
        {
            case States.Run:
                anim.SetFloat("state", 1f);
                break;

            case States.Attack:
                anim.SetFloat("state", 2f);
                break;

            case States.Idle:
            default: //idle
                anim.SetFloat("state", 0f);
                break;
        }
    }

    void Fight()
    {
        //unit has to be angry somehow
        state = States.Attack;

        //only grab the base tower and the basic unit that collides with
        //the existing unit
        var enemyBase = rayCast.collider.GetComponent<BaseTower>();
        var enemy = rayCast.collider.GetComponent<BasicUnit>();
        
        //if there is an enemy tower
        if (canFight && enemyBase != null )
        {
            //check if the enemy base's health is less than 0
            if (enemyBase.health <= 0)
            {
                StopAllCoroutines();
            }
            else
            //otherwise, try to kill the enemy base
            {
                enemyBase.health -= damageMultiplier();
            }
            //cooldown after attacking (1 second)
            StartCoroutine(fightCooldown());
        }
        //if there isan enemy
        else if (canFight && enemy != null)//&& enemy.side == getEnemySide())
        {
            //if the enemy is still alive, kill it
            if (enemy.health > 0)
            {
                enemy.health -= damageMultiplier();
            }
            StartCoroutine(fightCooldown());
        }
    }

    IEnumerator fightCooldown()
    {
        canFight = false;
        //yield return new WaitForSeconds(0.0f);
        Sound_Hit.PlayOneShot(Sound_Hit.clip, 0.5f);
        yield return new WaitForSecondsRealtime(1.0f);
        canFight = true;
    }

    /*
     * oppositeTag()
     * 
     * takes an existing collider and checks the tag of the collider
     * returns true if the unit and the colider have opposite tags
     */
    bool oppositeTag(Collider2D col)
    {
        //if the colliding object exists
        if (col != null)
        {
            //check the unit's side and compare the colider's tag to either a duck or player
            if (unit.side == CurrentSide.Human && col.CompareTag("Duck"))
            {
                return true;
            }
            else if (unit.side == CurrentSide.Duck && col.CompareTag("Player"))
            {
                return true;
            }
            //if the unit is a duck
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /*
     * getEnemySide()
     * 
     * Unlike oppositeTag(), this returns the opposite side
     * of the unit's side
     */
    CurrentSide getEnemySide()
    {
        if (unit.side == CurrentSide.Human)
            return CurrentSide.Duck;
        else
            return CurrentSide.Human;
    }

    //small fucntion to multiply the damage of each unit depending on the difficulty
    int damageMultiplier()
    {
        return (int)(dmg * (int)(Player.Instance.getDifficulty())/2);
    }
}
