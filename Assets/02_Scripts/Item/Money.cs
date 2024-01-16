using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int dropCoin;
    CoinText coinText;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        coinText = FindObjectOfType<CoinText>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            audioSource.Play();
            player.coinAmount += (dropCoin * player.currentCoinPoint);
            coinText.ChangeCoinUI();
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;

            Invoke("ReleaseCoin", 1f);
        }
    }

    private void ReleaseCoin()
    {
        gameObject.SetActive(false);
    }
}
