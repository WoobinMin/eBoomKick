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

    public override void Movement()
    {
        this.GetComponent<SpriteRenderer>().flipX = dir.x > 0;
        this.transform.Translate(dir * speed * Time.deltaTime);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (hp.curHP <= 0) return;

        Movement();
        Attack();
    }
}
