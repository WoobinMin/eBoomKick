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

    public override void Movement()
    {
        if(IsTargetVisible())
        {
            if (playerTrans == null)
                playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

            dir = playerTrans.position - this.transform.position;
            dir = dir.normalized;
            this.GetComponent<SpriteRenderer>().flipX = dir.x < 0;
        }

        this.transform.Translate(dir * speed * Time.deltaTime);
    }

    void Update()
    {
        if (hp.curHP <= 0)
        {
            if (!deadSound.isPlaying) deadSound.Play();
            return;
        }

        Movement();
        Attack();
        idleSound.mute = !IsTargetVisible();
        deadSound.mute = !IsTargetVisible();
    }
}
