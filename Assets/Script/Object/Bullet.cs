using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BoxCollider2D coll;
    public float speed;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        this.transform.Translate(Vector3.right *speed* Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.tag.Equals("Player") && !collision.gameObject.name.Contains("Bullet"))
        {
            var hitColls = Physics2D.OverlapBoxAll(this.transform.position, Vector2.one * 1f, 0);
            var rocketColls = Physics2D.OverlapBoxAll(this.transform.position, Vector2.one * 2f, 0);

            foreach (var rocketColl in rocketColls)
            {
                if(rocketColl.gameObject.tag.Equals("Player"))
                {
                    Debug.Log("RocketPlayer");
                    PlayerObject player = rocketColl.GetComponent<PlayerObject>();
                    Vector3 dir = rocketColl.gameObject.transform.position - this.transform.position;
                    dir = dir.normalized;
                    if(player.IsGrounded())
                    {
                        player.moveDirection += new Vector2(dir.x, dir.y);
                    }
                    else
                    {
                        player.moveDirection += new Vector2(dir.x, dir.y)*2;
                    }
                }
            }

            foreach (var hitColl in hitColls)
            {
                if (hitColl.gameObject.tag.Equals("Player"))
                {
                    Debug.Log("HitPlayer");
                }
            }

            this.gameObject.SetActive(false);
        }
    }
}
