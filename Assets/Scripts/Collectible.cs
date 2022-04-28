using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ê°È¡²ÝÝ® Åö×²¼ì²â
/// </summary>
public class Collectible : MonoBehaviour
{
    public ParticleSystem collectEffect = null;// Ê°È¡ÌØÐ§

    public AudioClip collectClip = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        if (pc != null)
        {
            if (pc.CurHp < pc.MaxHp)
            {
                pc.ChangeHp(1);
                Instantiate(collectEffect, transform.position, Quaternion.identity);
                AudioManager.Instance.AudioPlay(collectClip);
                Destroy(this.gameObject);
            }
        }
    }
}
