using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI管理
/// </summary>
public class UIManager : MonoBehaviour
{
    // 单例
    public static UIManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public Image hpBar; // 血条

    public Text bulletCountText;

    public void UpdateHpBar(int curAmount, int maxAmount)
    {
        hpBar.fillAmount = (float)curAmount / (float)maxAmount;
    }

    public void UpdateBulletCount(int curAmount, int maxAmount)
    {
        bulletCountText.text = curAmount.ToString() + " / " + maxAmount.ToString();
    }
}
