using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Skeleton,
    Dragon,
    Bat,
    GreenSlime,
    BlueSlime,
    GoldSlime,
    GoldBat
}

public class Monster : MonoBehaviour
{
    public int currentHp;
    public int maxHp;
    public int dropGold;

    public int damage;
    public Type type;
    public int actionCount;
    public int originCount;
    public bool isFull;
    public float distance;
    public bool isAlreadySpotted;

    public Player target;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        currentHp = maxHp;
        actionCount = originCount;
        distance = 3.9f;
    }

    public void Start()
    {
        target = FindObjectOfType<Player>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = Color.black;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    public void Die()
    {
        GameManager.Instance.player.killCount++;
        GameManager.Instance.player.CheckMultiPoint();

        switch (type)
        {
            case Type.Skeleton:
                GameManager.Instance.skeletons.Remove(gameObject.GetComponent<Skeleton>());
                break;
            case Type.Bat:
                GameManager.Instance.bats.Remove(gameObject.GetComponent<Bat>());
                break;
            case Type.Dragon:
                GameManager.Instance.dragons.Remove(gameObject.GetComponent<Dragon>());
                break;
            case Type.GreenSlime:
                GameManager.Instance.greenSlimes.Remove(gameObject.GetComponent<GreenSlime>());
                break;
            case Type.BlueSlime:
                GameManager.Instance.blueSlimes.Remove(gameObject.GetComponent<BlueSlime>());
                break;
            case Type.GoldBat:
                GameManager.Instance.goldBats.Remove(gameObject.GetComponent<GoldBat>());
                break;
            default:
                return;
        }

        GameObject money = GameManager.Instance.pool.Get(1);
        money.GetComponent<Money>().dropCoin = dropGold;
        money.transform.position = transform.position;

        Destroy(gameObject);
    }

    public void CheckNextPos(Vector2 nextPos)
    {
        if (GameManager.Instance.monstersNextPos.Contains(nextPos))
        {
            isFull = true;
        }
    }

    public void CheckDistance()
    {
        if (distance >= (GameManager.Instance.player.transform.position - transform.position).magnitude)
        {
            spriteRenderer.color = Color.white;
            isAlreadySpotted = true;
        }
        else if (isAlreadySpotted)
        {
            spriteRenderer.color = target.GetComponent<ChangeColorNearPlayer>().alreadyCheckView;
        }
        else { return; }
    }
}
