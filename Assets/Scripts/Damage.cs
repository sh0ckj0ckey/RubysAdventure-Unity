using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ÉËº¦ÏÝÚå¼ì²â
/// </summary>
public class Damage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        try
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.ChangeHp(-1);
            }
        }
        catch { }
    }
}
