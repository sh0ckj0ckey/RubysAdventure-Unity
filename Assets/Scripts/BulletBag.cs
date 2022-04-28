using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBag : MonoBehaviour
{
    public ParticleSystem collectEffect = null;// Ê°È¡ÌØÐ§

    public AudioClip collectClip = null;

    public int bulletCount = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (playerController.CurBullet < playerController.MaxBullet)
            {
                playerController.ChangeBulletCount(bulletCount);
                Instantiate(collectEffect, transform.position, Quaternion.identity);
                AudioManager.Instance.AudioPlay(collectClip);
                Destroy(this.gameObject);
            }
        }
    }
}
