using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class ObstacleObject : MonoBehaviour
{
    [SerializeField] public HP hp;

    public int parentIndex;
    public AudioSource idleSound;
    public AudioSource deadSound;
    public abstract void Movement();
    public abstract void Attack();

    public virtual void Dead()
    {
        if(hp.curHP <= 0)
        {
            MonsterSpawner.Instance.StartCoroutine(MonsterSpawner.Instance.SpawningSpecificTrans(0f, parentIndex));
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if(collision.gameObject.name.Contains("Bullet"))
        {
            this.GetComponent<Animator>().SetTrigger("Attacked");
            hp.curHP--;
        }
        else if(collision.gameObject.tag.Equals("Player"))
        {
            PlayerObject player = collision.GetComponent<PlayerObject>();
            player.GetComponent<Animator>().SetTrigger("Attacked");
            player.hp.curHP-=10;
        }
        else if(collision.gameObject.tag.Equals("Wall"))
        {
            MonsterSpawner.Instance.StartCoroutine(MonsterSpawner.Instance.SpawningSpecificTrans(5f, parentIndex));
            this.gameObject.SetActive(false);
        }
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
}
