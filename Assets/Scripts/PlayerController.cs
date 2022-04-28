using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;// ����

    private int maxHp = 5;  // �������ֵ
    private int curHp = 2;  // ��ǰ����ֵ

    public int MaxHp { get { return maxHp; } }
    public int CurHp { get { return curHp; } }


    [SerializeField] //�����Ļ���ʹ��privateҲ������unity�������������ֱ�ӵ���curBulletCount��ֵ
    private int curBulletCount = 6;
    
    private int maxBulletCount = 99;

    public int MaxBullet { get { return maxBulletCount; } }
    public int CurBullet { get { return curBulletCount; } }

    private float invincibleTime = 2f;//�ܵ��˺�����޵�ʱ��
    private float invincibleTimer;//�޵м�ʱ

    private bool isInvincible;

    public AudioClip hitClip;   //������Ч
    public AudioClip launchClip;//������Ч

    public GameObject bulletPrefab = null; //�ӵ�

    private Vector2 lookDirection = new Vector2(1, 0); //Ĭ�ϳ���

    Rigidbody2D rbody;      // �������

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
                isInvincible = false;//�޵�ʱ�������ȡ���޵�״̬
            }
        }

        //��ⰴ�¿ո񹥻�
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

        //����E������
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
                //�ܵ��˺�
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
            UIManager.Instance.UpdateHpBar(curHp, maxHp); // ����Ѫ��
        }
        catch { }
    }

    public void ChangeBulletCount(int amount)
    {
        curBulletCount = Mathf.Clamp(curBulletCount + amount, 0, maxBulletCount);
        UIManager.Instance.UpdateBulletCount(curBulletCount, maxBulletCount);
    }
}
