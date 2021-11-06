using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class ObstacleObject : MonoBehaviour
{
    public int curHP;
    public int maxHP;

    public abstract void Movement();
    public abstract void Attack();
    public abstract void Dead();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.gameObject.name.Contains("Bullet"))
        {
            collision.gameObject.SetActive(false);
            curHP--;
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
