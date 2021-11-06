using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBullet : MonoBehaviour
{
    public float speed;

    void Start()
    {
        
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

    void Update()
    {
        this.transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (!IsTargetVisible())
            this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.tag.Equals("Player"))
        {
            PlayerObject player = collision.GetComponent<PlayerObject>();
            player.GetComponent<Animator>().SetTrigger("Attacked");
            player.hp.curHP -= 10;
            this.gameObject.SetActive(false);
        }

    }
}
