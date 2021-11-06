using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDron : ObstacleObject
{
    private Transform playerTrans;
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
        if(IsTargetVisible())
        {
            if (playerTrans == null)
                playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

            dir = playerTrans.position - this.transform.position;
            dir = dir.normalized;
            
        }

        this.transform.Translate(dir * speed * Time.deltaTime);
    }

    void Update()
    {
        if(curHP <= 0)
        {
            Dead();
            return;
        }

        Movement();
        Attack();
    }
}
