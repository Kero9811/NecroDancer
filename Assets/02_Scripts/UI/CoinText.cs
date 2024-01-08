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
        coinText = GameManager.Instance.player.coinAmount;
    }

    private void Update()
    {
        text.text = $"x {coinText}";
    }
}
