using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCar : ObstacleObject
{
    public Timer randTimer;
    public Vector2 randDir;
    public float speed;

    public override void Attack()
    {
    }

    public override void Movement()
    {
        if(randTimer.curTime > randTimer.lastTime)
        {
            randTimer.curTime = 0f;
            randDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            randDir = randDir.normalized;
        }
        else
        {
            randTimer.curTime += Time.deltaTime;
        }

        this.transform.Translate(randDir * speed * Time.deltaTime);
    }

    void Start()
    {
        randTimer.curTime = 0f;
        randTimer.lastTime = Random.Range(1f, 3f);
        randDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        randDir = randDir.normalized;
    }

    void Update()
    {
        if (hp.curHP <= 0) return;

        Attack();
        Movement();
    }
}
