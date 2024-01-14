using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    TextMeshProUGUI text;
    int coinText;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        ChangeCoinUI();
    }
    public void ChangeCoinUI()
    {
        coinText = GameManager.Instance.player.coinAmount;
        text.text = $"x {coinText}";
    }
}
