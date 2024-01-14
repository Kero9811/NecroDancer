using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int dropCoin;
    CoinText coinText;

    private void Start()
    {
        coinText = FindObjectOfType<CoinText>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.coinAmount += (dropCoin * player.currentCoinPoint);
            coinText.ChangeCoinUI();
            gameObject.SetActive(false);
        }
    }
}
