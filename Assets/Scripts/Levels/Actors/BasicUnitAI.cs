using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States { Idle, Run, Attack, Stun, Die}

public class BasicUnitAI : MonoBehaviour
{
    public float speed;
    public int dmg;
    public Animator anim;
    public LayerMask layer;

    private BasicUnit unit;
    private Vector2 dir;
    private RaycastHit2D rayCast;
    private bool fight;
    private States state;

    // Start is called before the first frame update
    void Start()
    {
        state = States.Idle;
        unit = GetComponent<BasicUnit>();
        dir = unit.side == CurrentSide.Human ? Vector2.right : Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        rayCast = Physics2D.Raycast(getRayCastPosition(), dir, 0.01f, layer);
        //if nothing is being hit
        Debug.Log(rayCast.collider);
        if (rayCast.collider != null)
        {
            state = States.Attack;
            Fight();
        }
        else {
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
        var enemyBase = rayCast.collider.GetComponent<BaseTower>();
        var enemy = rayCast.collider.GetComponent<BasicUnit>();
        if (enemyBase != null && enemyBase.side == getEnemySide())
        {
            if (enemyBase.health <= 0)
            {
                Destroy(enemyBase.gameObject);
                speed = 0;
                state = States.Idle;
            }
            else
            {
                enemyBase.health -= 5;
                //yield return new WaitForSeconds(5);
            }
        }
        else if (enemy != null && enemy.side == getEnemySide())
        { 
            if (enemy.health <= 0)
            {
                Destroy(enemy.gameObject);
            }
            else
            {
                enemy.health -= 5;
                //yield return new WaitForSeconds(5);
            }
        }
    }


    CurrentSide getEnemySide()
    {
        if (unit.side == CurrentSide.Human)
            return CurrentSide.Duck;
        else
            return CurrentSide.Human;
    }
}
