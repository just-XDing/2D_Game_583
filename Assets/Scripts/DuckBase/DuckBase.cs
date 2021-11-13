using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBase : MonoBehaviour
{
    public Animator Duck_Anim;
    public Rigidbody2D Duck_RigBody;

    private HealthBase Health;
    private float Speed;

    // Start is called before the first frame update
    void Start()
    {
        Duck_RigBody = GetComponent<Rigidbody2D>();
        //Set the speed of the GameObject
        Speed = -1.0f;
        Health = new HealthBase(40);
    }

    // Update is called once per frame
    void Update()
    {
        Duck_RigBody.velocity = transform.right * Speed;
        if (Health.getHealth() <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void takeHealth(int h)
    {
        Health.takeHealth(h);
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Duck_Anim.SetBool("duckColliding", true);
            Duck_Anim.SetBool("duckAttacking", true);
            foreach (HumanBase g in col.gameObject.GetComponents<HumanBase>())
            {
                g.takeHealth(1);
            }
        }
        else
        {
            Duck_Anim.SetBool("duckAttacking", false);
            Duck_Anim.SetBool("duckColliding", false);
        }
    }
}
