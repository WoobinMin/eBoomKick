using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    [HideInInspector] public BoxCollider2D coll;
    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public Vector2 moveDirection;
    [HideInInspector] public Timer shotTimer;
    [HideInInspector] public Animator anim;
    [HideInInspector] public ParticleSystemMananger psm;

    [Header("PlayerInformation")]
    [Tooltip("Player Jump Power")] public float jumpPower;
    [Tooltip("Player Speed")] public float speed;
    [Tooltip("Ground��� �ν��� Layer")] public LayerMask gourndCheckLayer;
    [Tooltip("Gun Object")] public GameObject gunObject;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        psm = GetComponent<ParticleSystemMananger>();
        shotTimer.lastTime = 1.5f;
        shotTimer.curTime = shotTimer.lastTime;
    }

    void Update()
    {
        Gravity();
        HorizontalMovement();
        Jump();
        Shooting();

        transform.Translate(moveDirection * Time.deltaTime);
    }

    private void HorizontalMovement()
    {
        if(IsGrounded())
        {
            float hori = Input.GetAxisRaw("Horizontal");
            moveDirection.x = hori * speed;
            anim.SetBool("isMove", hori != 0);
            sr.flipX = hori < 0;
        }
    }

    private void Gravity()
    {
        moveDirection.y -= 9.8f * Time.deltaTime;
    }

    private void Jump()
    {
        if (this.IsGrounded())
        {
            if (this.moveDirection.y < 0)
            {
                this.moveDirection.y = 0f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.moveDirection.y = this.jumpPower;
            }
        }
    }

    private void Shooting()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mousePos - this.transform.position;

        float AngleRad = Mathf.Atan2(mousePos.y - gunObject.transform.position.y, mousePos.x - gunObject.transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        gunObject.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        gunObject.GetComponent<SpriteRenderer>().flipY = (AngleDeg > 90 || AngleDeg < -90);


        if (shotTimer.curTime > shotTimer.lastTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                shotTimer.curTime = 0f;
                GameObject bullet = ObjectPooler.Instance.GetPooledObject("Bullet");
                Bullet bulletCom = bullet.GetComponent<Bullet>();

                bulletCom.dir = dir;
                bulletCom.speed = 4f;
                bulletCom.transform.position = this.transform.position;
                bullet.gameObject.SetActive(true);

                this.moveDirection += new Vector2(dir.x, dir.y) * -1f;

                psm.AffectParticle("boom", gunObject.transform, Vector3.zero, Quaternion.Euler(0, 0, AngleDeg + 90));
            }
        }
        else
        {
            shotTimer.curTime += Time.deltaTime;
        }

    }


    public bool IsGrounded()
    {
        float extraHeightText = 0.01f;
        Vector3 centerPos = coll.bounds.center + coll.bounds.extents.y * Vector3.down;
        RaycastHit2D raycastHit = Physics2D.BoxCast(centerPos,
            coll.bounds.size * 0.02f, 0f, Vector2.down, extraHeightText, gourndCheckLayer);

        return raycastHit.collider != null;
    }


}
