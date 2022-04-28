using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    public GameObject TipImage = null;//提示按E
    public GameObject dialogImage = null;// 对话框

    public float showTime = 4;//对话框显示时间

    private float showTimer;

    // Start is called before the first frame update
    void Start()
    {
        TipImage.SetActive(true);
        dialogImage.SetActive(false); // 初始默认隐藏
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
