using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;// 移速

    private int maxHp = 5;  // 最大生命值
    private int curHp = 2;  // 当前生命值

    public int MaxHp { get { return maxHp; } }
    public int CurHp { get { return curHp; } }


    [SerializeField] //这样的话即使是private也可以在unity的属性面板里面直接调整curBulletCount数值
    private int curBulletCount = 6;
    
    private int maxBulletCount = 99;

    public int MaxBullet { get { return maxBulletCount; } }
    public int CurBullet { get { return curBulletCount; } }

    private float invincibleTime = 2f;//受到伤害后的无敌时间
    private float invincibleTimer;//无敌计时

    private bool isInvincible;

    public AudioClip hitClip;   //受伤音效
    public AudioClip launchClip;//攻击音效

    public GameObject bulletPrefab = null; //子弹

    private Vector2 lookDirection = new Vector2(1, 0); //默认朝右

    Rigidbody2D rbody;      // 刚体组件

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        curHp = 2;
        invincibleTimer = 0;
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        UIManager.Instance.UpdateHpBar(curHp, maxHp);
        UIManager.Instance.UpdateBulletCount(curBulletCount, maxBulletCount);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveVector = new Vector2(moveX, moveY);
        if (moveVector.x != 0 || moveVector.y != 0)
        {
            lookDirection = moveVector;
        }
        anim.SetFloat("Look X", lookDirection.x);
        anim.SetFloat("Look Y", lookDirection.y);
        anim.SetFloat("Speed", moveVector.magnitude);

        float spd = 5f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            spd = 7f;
        }

        Vector2 position = rbody.position;
        //position.x += moveX * speed * Time.deltaTime;
        //position.y += moveY * speed * Time.deltaTime;
        position += moveVector * spd * Time.deltaTime;
        rbody.MovePosition(position);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;//无敌时间结束后取消无敌状态
            }
        }

        //检测按下空格攻击
        if (Input.GetKeyDown(KeyCode.Space) && curBulletCount > 0)
        {
            ChangeBulletCount(-1);
            anim.SetTrigger("Launch");
            AudioManager.Instance.AudioPlay(launchClip);
            GameObject bullet = Instantiate(bulletPrefab, rbody.position + Vector2.up * 0.5f, Quaternion.identity);
            BulletController bc = bullet.GetComponent<BulletController>();
            if (bc != null)
            {
                bc.Move(lookDirection, 300);
            }
        }

        //按下E键交互
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rbody.position, lookDirection, 2f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NPCManager npc = hit.collider.GetComponent<NPCManager>();
                if (npc != null)
                {
                    npc.ShowDialog();
                }
            }
        }
    }

    public void ChangeHp(int addingHp)
    {
        try
        {
            if (addingHp < 0)
            {
                //受到伤害
                if (isInvincible)
                {
                    return;
                }
                else
                {
                    isInvincible = true;
                    anim.SetTrigger("Hit");
                    AudioManager.Instance.AudioPlay(hitClip);
                    invincibleTimer = invincibleTime;
                }
            }

            curHp = Mathf.Clamp(curHp + addingHp, 0, maxHp);
            UIManager.Instance.UpdateHpBar(curHp, maxHp); // 更新血条
        }
        catch { }
    }

    public void ChangeBulletCount(int amount)
    {
        curBulletCount = Mathf.Clamp(curBulletCount + amount, 0, maxBulletCount);
        UIManager.Instance.UpdateBulletCount(curBulletCount, maxBulletCount);
    }
}
