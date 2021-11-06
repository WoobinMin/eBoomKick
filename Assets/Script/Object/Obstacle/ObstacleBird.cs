using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBird : ObstacleObject
{
    public Vector2 dir;
    public float speed;

    public override void Attack()
    {

    }

    public override void Dead()
    {

    }

    public override void Movement()
    {
        this.transform.Translate(dir * speed * Time.deltaTime);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (curHP <= 0)
        {
            Dead();
            return;
        }

        Movement();
        Attack();
    }
}
