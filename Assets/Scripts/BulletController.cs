using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rbody;

    public AudioClip hitClip;

    // 不用Start而用Awake的原因，Awake在Start之前执行，在PlayerController按下空格时创建的对象
    // 还没来得及执行Start就调用了Move，所以没有获取到刚体，而Awake执行的更早，这样就可以使用Move使其运动
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //子弹移动
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
