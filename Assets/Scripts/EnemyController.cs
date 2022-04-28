using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人控制
/// </summary>
public class EnemyController : MonoBehaviour
{
    public float speed = 3;

    public bool isVertical;// 是否是垂直移动

    private Vector2 moveDirection;// 移动方向

    public float changeDirectionTime = 2f;//改变方向的时间
    public float changeDirectionTimer;

    private Rigidbody2D rbody;

    private Animator anim;

    public ParticleSystem brokenEffect;// 损坏效果

    private bool isFixed = false;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveDirection = isVertical ? Vector2.up : Vector2.right; //如果是垂直移动，则朝上，否则朝右
        changeDirectionTimer = changeDirectionTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFixed) return;
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer < 0)
        {
            moveDirection *= -1;
            changeDirectionTimer = changeDirectionTime;
        }
        Vector2 position = rbody.position;
        position.x += moveDirection.x * speed * Time.deltaTime;
        position.y += moveDirection.y * speed * Time.deltaTime;
        rbody.MovePosition(position);
        anim.SetFloat("moveX", moveDirection.x);
        anim.SetFloat("moveY", moveDirection.y);
    }

    // 两个刚体碰撞 信息更详细
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.ChangeHp(-1);
        }
    }

    // 被修好
    public void Fixed()
    {
        try
        {
            isFixed = true;
            if (brokenEffect.isPlaying)
            {
                brokenEffect.Stop();
            }
            rbody.simulated = false;// 禁用物理
            anim.SetTrigger("fix");
        }
        catch { }
    }
}
