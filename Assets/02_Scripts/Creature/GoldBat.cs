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
        GameManager.Instance.bats.Add(gameObject.GetComponent<GoldBat>());
        monsterHpUI = GetComponentInChildren<MonsterHpUI>();
        audioSource = GetComponent<AudioSource>();
        canvas = GetComponentInChildren<Canvas>();
        monsterCollider = GetComponent<CapsuleCollider2D>();
        exit = FindObjectOfType<Exit>();
    }

    public override void CheckDistance()
    {
        // -1칸씩 만큼 어긋나 있음 (임시조치 완)
        if (changeColorNearPlayer.playerCellList.
            Contains(new Vector3Int((int)transform.position.x - 1, (int)transform.position.y - 1, (int)transform.position.z)))
        {
            spriteRenderer.color = Color.white;
            canvas.enabled = true;
        }
        else
        {
            Color myColor = spriteRenderer.color;
            myColor.a = 0f;
            spriteRenderer.color = myColor;
            canvas.enabled = false;
        }
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

        canvas.gameObject.SetActive(false);
        spriteRenderer.enabled = false;
        monsterCollider.enabled = false;

        Invoke("DestroyObject", 3f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
