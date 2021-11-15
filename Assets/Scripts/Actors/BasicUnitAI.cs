using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States { Idle, Run, Attack, Die}

public class BasicUnitAI : MonoBehaviour
{
    public float speed;
    public int dmg;
    public Animator anim;
    public LayerMask layer;
    public GameObject explosion;


    private BasicUnit unit;
    private Vector2 dir;
    private RaycastHit2D rayCast;
    private bool roundEnd;
    private bool canFight;
    private States state;

    // Start is called before the first frame update
    void Start()
    {
        roundEnd = false;
        canFight = true;
        state = States.Idle;
        unit = GetComponent<BasicUnit>();
        dir = unit.side == CurrentSide.Human ? Vector2.right : Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
        if (unit.health <= 0)
        {
            Die();
        }
        UpdateState();
        rayCast = Physics2D.Raycast(getRayCastPosition(), dir, 0.01f, layer);

        //if nothing is being hit
        if (!roundEnd && rayCast.collider != null && oppositeTag(rayCast.collider))
        {
            Fight();
        }
        else if (!roundEnd)
        {
            if (rayCast.collider != null && !oppositeTag(rayCast.collider))
            {
                Physics2D.IgnoreCollision(rayCast.collider, this.GetComponent<BoxCollider2D>());
            }

            state = States.Run;
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
            state = States.Idle;
        }
    }

    void Die()
    {
        canFight = false;
        state = States.Die;
        explosion.transform.localScale = new Vector3(3, 3, 1);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    bool oppositeTag(Collider2D col)
    {
        if (unit.side == CurrentSide.Human && col.CompareTag("Duck"))
        {
            return true;
        }
        else if (unit.side == CurrentSide.Duck && col.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    Vector3 getRayCastPosition()
    {
        switch (unit.side)
        {
            case CurrentSide.Human:
                return unit.transform.position + new Vector3(0.2f, 0);
                
            case CurrentSide.Duck:
                return unit.transform.position + new Vector3(-0.2f, 0);
            default:
                return unit.transform.position;
        }
    }

    void UpdateState()
    {
        switch(state)
        {
            case States.Run:
                anim.SetBool("attack", false);
                anim.SetBool("collide", false);
                break;

            case States.Attack:
                anim.SetBool("collide", true);
                anim.SetBool("attack", true);
                break;

            case States.Idle:
            default: //idle
                anim.SetBool("attack", false);
                anim.SetBool("collide", true);
                break;
        }
    }


    
    void Fight()
    {
        state = States.Attack;
        var enemyBase = rayCast.collider.GetComponent<BaseTower>();
        var enemy = rayCast.collider.GetComponent<BasicUnit>();
        if (canFight && enemyBase != null )//&& enemyBase.side == getEnemySide())
        {
            if (enemyBase.health <= 0)
            {
                Destroy(enemyBase.gameObject);
                speed = 0;
                state = States.Idle;
                roundEnd = true;
            }
            else
            {
                enemyBase.health -= dmg;
            }
            StartCoroutine(fightCooldown());
        }
        else if (canFight && enemy != null)//&& enemy.side == getEnemySide())
        { 
            if (enemy.health <= 0)
            {
                Destroy(enemy.gameObject);
            }
            else
            {
                enemy.health -= dmg;
            }
            StartCoroutine(fightCooldown());
        }
    }

    IEnumerator fightCooldown()
    {
        canFight = false;
        yield return new WaitForSeconds(2.0f);
        canFight = true;
    }

    CurrentSide getEnemySide()
    {
        if (unit.side == CurrentSide.Human)
            return CurrentSide.Duck;
        else
            return CurrentSide.Human;
    }
}
