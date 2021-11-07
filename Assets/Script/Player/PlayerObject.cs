using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerObject : MonoBehaviour
{
    [HideInInspector] public BoxCollider2D coll;
    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public Vector2 moveDirection;
    [HideInInspector] public Timer shotTimer;
    [HideInInspector] public Animator anim;
    [HideInInspector] public HP hp;
    [HideInInspector] public AudioSource walkSound;
    [HideInInspector] public bool canAttacked;
    [HideInInspector] public bool deadStart;

    [Header("PlayerInformation")]
    [Tooltip("Player Jump Power")] public float jumpPower;
    [Tooltip("Player Speed")] public float speed;
    [Tooltip("Ground라고 인식할 Layer")] public LayerMask gourndCheckLayer;
    [Tooltip("Gun Object")] public GameObject gunObject;
    [Tooltip("HP Bar")] public Image hpBar;
    [Tooltip("Reload Progress Bar")] public Image reloadImage;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        walkSound = GetComponentInChildren<AudioSource>();
        shotTimer.lastTime = 0.5f;
        shotTimer.curTime = shotTimer.lastTime;
        hp.curHP = 100;
        hp.maxHP = 100;
        canAttacked = true;
        deadStart = false;

        Animation fadeAnim = GameObject.Find("Fade").GetComponent<Animation>();
        fadeAnim.Play("Fade_End");
    }

    void Update()
    {
        if(CheckDead())
        {
            if (!deadStart)
                StartCoroutine(Dead());

            return;
        }
        Gravity();
        HorizontalMovement();
        Jump();
        Shooting();

        if (moveDirection.y < -9.8f / 2f ) moveDirection.y = -9.8f / 2f;
        transform.Translate(moveDirection * Time.deltaTime);
    }

    public IEnumerator Dead()
    {
        deadStart = true;
        Animation fadeAnim = GameObject.Find("Fade").GetComponent<Animation>();
        fadeAnim.Play("Fade_Begin");
        yield return new WaitForSeconds(fadeAnim.GetClip("Fade_Begin").length);
        SceneManager.LoadScene("FadeScene");
    }

    private bool CheckDead()
    {
        hpBar.fillAmount = (float)hp.curHP / hp.maxHP;
        if(hp.curHP <= 0 || this.transform.position.y  < -22f)
        {
            return true;
        }
        return false;
    }

    private void HorizontalMovement()
    {
        if(IsGrounded())
        {
            float hori = Input.GetAxisRaw("Horizontal");

            walkSound.mute = hori == 0;
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
                SoundController.instance.SoundControll("Eff_Jump");
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
        reloadImage.fillAmount = shotTimer.curTime / shotTimer.lastTime;

        if (shotTimer.curTime > shotTimer.lastTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                shotTimer.curTime = 0f;
                GameObject bullet = ObjectPooler.Instance.GetPooledObject("Bullet");
                Bullet bulletCom = bullet.GetComponent<Bullet>();

                bulletCom.speed = 4f;
                bullet.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
                bulletCom.transform.position = this.transform.position;
                bullet.gameObject.SetActive(true);

                reloadImage.fillAmount = 0;

                if (IsGrounded() && dir.y > 0) dir.y = 0;
                this.moveDirection += new Vector2(dir.x, dir.y) * -1f;

                SoundController.instance.SoundControll("Eff_Shot");
                ParticleSystemMananger.Instance.AffectParticle("boom", this.transform, Vector3.zero, Quaternion.Euler(0, 0, AngleDeg+90));
            }
        }
        else
        {
            shotTimer.curTime += Time.deltaTime;
        }

    }

    public IEnumerator Attacked()
    {
        canAttacked = false;
        hp.curHP -= 10;
        anim.SetTrigger("Attacked");
        sr.DOFade(0.5f, 0.25f);
        yield return new WaitForSeconds(0.25f);
        sr.DOFade(1f, 0.25f);
        yield return new WaitForSeconds(0.25f);
        sr.DOFade(0.5f, 0.25f);
        yield return new WaitForSeconds(0.25f);
        sr.DOFade(1f, 0.25f);
        yield return new WaitForSeconds(0.25f);

        canAttacked = true;
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
