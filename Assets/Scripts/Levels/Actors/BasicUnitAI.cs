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
        dir = unit.cur == CurrentSide.Human ? Vector2.right : Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        rayCast = Physics2D.Raycast(unit.transform.position, dir, 1.0f, layer);
        Debug.Log(rayCast.collider);
        //if nothing is being hit
        if (rayCast.collider != null)
        {
            state = States.Attack;
            Fight();
        }
        else {

            state = States.Run;
            if (unit.cur == CurrentSide.Human)
            {
                transform.position += Vector3.left * speed;
            }
            else
            {
                transform.position += Vector3.right * speed;
            }
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
        if (enemyBase != null)
        {
            if (enemyBase.health <= 0)
            {
                Destroy(enemyBase.gameObject);
                this.speed = 0;
            }
            else
            {
                enemyBase.health -= 1;
            }
        }
        if (enemy != null)
        { 
            if (enemy.health <= 0)
            {
                Destroy(enemy.gameObject);
            }
            else
            {
                enemy.health -= 1;
            }
        }
    }
}
