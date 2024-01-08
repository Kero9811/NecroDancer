using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinPoint : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = GameManager.Instance.player.currentCoinPoint.ToString();
    }
}
