using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    public GameObject TipImage = null;//��ʾ��E
    public GameObject dialogImage = null;// �Ի���

    public float showTime = 4;//�Ի�����ʾʱ��

    private float showTimer;

    // Start is called before the first frame update
    void Start()
    {
        TipImage.SetActive(true);
        dialogImage.SetActive(false); // ��ʼĬ������
        showTimer = -1;
    }

    // Update is called once per frame
    void Update()
    {
        showTimer -= Time.deltaTime;
        if (showTimer < 0)
        {
            TipImage.SetActive(true);
            dialogImage.SetActive(false);
        }
    }

    public void ShowDialog()
    {
        showTimer = showTime;
        TipImage.SetActive(false);
        dialogImage.SetActive(true);
    }
}
