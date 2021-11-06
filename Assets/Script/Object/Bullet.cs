using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BoxCollider2D coll;
    public ParticleSystemMananger psm;
    public float speed;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        psm = GetComponent<ParticleSystemMananger>();
    }

    void Update()
    {
        this.transform.Translate(Vector3.right *speed* Time.deltaTime);
        if (!IsTargetVisible())
            this.gameObject.SetActive(false);
    }

    public bool IsTargetVisible()
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        var point = this.transform.position;
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        }
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.tag.Equals("Player") && !collision.gameObject.name.Contains("Bullet"))
        {
            ParticleSystemMananger.Instance.AffectParticle("explosion", this.transform, Vector3.zero);

            if (collision.gameObject.name.Equals("Blanket"))
            {
                Debug.Log("Å¬¸®¾î~");
                this.gameObject.SetActive(false);
                return;
            }


            var hitColls = Physics2D.OverlapBoxAll(this.transform.position, Vector2.one * 1f, 0);
            var rocketColls = Physics2D.OverlapBoxAll(this.transform.position, Vector2.one * 2f, 0);

            foreach (var rocketColl in rocketColls)
            {
                if(rocketColl.gameObject.tag.Equals("Player"))
                {
                    Vector3 dir = rocketColl.gameObject.transform.position - this.transform.position;
                    PlayerObject player = rocketColl.GetComponent<PlayerObject>();

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
                    hitColl.GetComponent<PlayerObject>().hp.curHP -= 5;
                }
            }

            this.gameObject.SetActive(false);
        }
    }
}
