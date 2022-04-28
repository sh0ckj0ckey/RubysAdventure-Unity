using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rbody;

    public AudioClip hitClip;

    // ����Start����Awake��ԭ��Awake��Start֮ǰִ�У���PlayerController���¿ո�ʱ�����Ķ���
    // ��û���ü�ִ��Start�͵�����Move������û�л�ȡ�����壬��Awakeִ�еĸ��磬�����Ϳ���ʹ��Moveʹ���˶�
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //�ӵ��ƶ�
    public void Move(Vector2 moveDirection, float moveForce)
    {
        rbody.AddForce(moveDirection * moveForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
        if (ec != null)
        {
            ec.Fixed();
        }
        AudioManager.Instance.AudioPlay(hitClip);
        Destroy(this.gameObject);
    }
}
