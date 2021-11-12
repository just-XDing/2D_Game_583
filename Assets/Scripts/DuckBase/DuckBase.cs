using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBase : MonoBehaviour
{
    public Animator Duck_Anim;
    public Rigidbody2D Duck_RigBody;

    public int Health;
    private float Speed;

    // Start is called before the first frame update
    void Start()
    {
        Duck_RigBody = GetComponent<Rigidbody2D>();
        //Set the speed of the GameObject
        Speed = -1.0f;
        Health = 20;
    }

    // Update is called once per frame
    void Update()
    {
        Duck_RigBody.velocity = transform.right * Speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Duck_Anim.SetBool("duckColliding", true);
            Attack(col);
        }
        else
        {
            Duck_Anim.SetBool("duckColliding", false);
        }
    }


    void Attack(Collision2D col)
    {
        if (col.gameObject.name == "Tower_P")
        {
            Duck_Anim.SetBool("duckAttacking", true);
        }
        else
        {
            Duck_Anim.SetBool("duckAttacking", false);
            Destroy(col.gameObject);
        }
    }
}
