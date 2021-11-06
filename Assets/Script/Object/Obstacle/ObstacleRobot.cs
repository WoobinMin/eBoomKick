using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRobot : ObstacleObject
{
    private Transform playerTrans;
    public Vector2 moveDir;
    public Vector2 atkDir;
    public Timer atkTimer;
    public float speed;

    public override void Attack()
    {
        if (atkTimer.curTime > atkTimer.lastTime)
        {
            if (IsTargetVisible())
            {
                if (playerTrans == null)
                    playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

                atkDir = playerTrans.position - this.transform.position;
                atkDir = atkDir.normalized;

                GameObject robotBullet = ObjectPooler.Instance.GetPooledObject("Robot_Bullet");
                RobotBullet robotBulletCom = robotBullet.GetComponent<RobotBullet>();

                float AngleRad = Mathf.Atan2(atkDir.y, atkDir.x);
                float AngleDeg = (180 / Mathf.PI) * AngleRad;
                robotBullet.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
                robotBullet.transform.position = this.transform.position;
                robotBullet.gameObject.SetActive(true);

                atkTimer.curTime = 0f;
            }
        }
        else
        {
            atkTimer.curTime += Time.deltaTime;
        }
    }

    public override void Movement()
    {
        this.transform.Translate(moveDir * speed * Time.deltaTime);
    }


    void Start()
    {
        atkTimer.lastTime = 1.5f;
        atkTimer.curTime = atkTimer.lastTime;
    }

    // Update is called once per frame
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
