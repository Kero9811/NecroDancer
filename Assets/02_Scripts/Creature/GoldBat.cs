using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBat : Bat
{
    MonsterHpUI monsterHpUI;
    AudioSource audioSource;
    Canvas canvas;
    CapsuleCollider2D monsterCollider;
    Exit exit;

    new void Start()
    {
        base.Start();
        monsterHpUI = GetComponentInChildren<MonsterHpUI>();
        audioSource = GetComponent<AudioSource>();
        canvas = GetComponentInChildren<Canvas>();
        monsterCollider = GetComponent<CapsuleCollider2D>();
        exit = FindObjectOfType<Exit>();
    }

    public override void TakeDamage(int damage)
    {
        currentHp -= damage;
        monsterHpUI.ChangeHpUI();

        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    public override void Die()
    {
        audioSource.Play();
        GameManager.Instance.player.killCount++;
        GameManager.Instance.player.CheckMultiPoint();
        exit.isExecuted = true;
        GameManager.Instance.bats.Remove(gameObject.GetComponent<Bat>());

        GameObject money = GameManager.Instance.pool.Get(1);
        money.GetComponent<Money>().dropCoin = dropGold;
        money.transform.position = transform.position;

        canvas.enabled = false;
        spriteRenderer.enabled = false;
        monsterCollider.enabled = false;

        Invoke("DestroyObject", 3f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
