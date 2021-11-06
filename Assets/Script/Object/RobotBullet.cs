using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBullet : MonoBehaviour
{
    public Vector2 dir;
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        dir = dir.normalized;
        this.transform.Translate(dir * speed * Time.deltaTime);
    }
}
