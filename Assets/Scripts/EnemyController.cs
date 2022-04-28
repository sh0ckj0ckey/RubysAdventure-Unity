using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���˿���
/// </summary>
public class EnemyController : MonoBehaviour
{
    public float speed = 3;

    public bool isVertical;// �Ƿ��Ǵ�ֱ�ƶ�

    private Vector2 moveDirection;// �ƶ�����

    public float changeDirectionTime = 2f;//�ı䷽���ʱ��
    public float changeDirectionTimer;

    private Rigidbody2D rbody;

    private Animator anim;

    public ParticleSystem brokenEffect;// ��Ч��

    private bool isFixed = false;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveDirection = isVertical ? Vector2.up : Vector2.right; //����Ǵ�ֱ�ƶ������ϣ�������
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

    // ����������ײ ��Ϣ����ϸ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.ChangeHp(-1);
        }
    }

    // ���޺�
    public void Fixed()
    {
        try
        {
            isFixed = true;
            if (brokenEffect.isPlaying)
            {
                brokenEffect.Stop();
            }
            rbody.simulated = false;// ��������
            anim.SetTrigger("fix");
        }
        catch { }
    }
}
