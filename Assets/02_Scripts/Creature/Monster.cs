using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Skeleton,
    Dragon,
    Bat
}

public abstract class Monster : MonoBehaviour
{
    public int currentHp;
    public int maxHp;

    public int damage;
    public Type type;
    public int actionCount;
    public int originCount;
    public bool isFull;

    private void Awake()
    {
        currentHp = maxHp;
        actionCount = originCount;
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
        GameManager.Instance.player.currentCoinPoint++;
        GameManager.Instance.player.CheckMultiPoint();

        if (type == Type.Skeleton)
        {
            GameManager.Instance.skeletons.Remove(gameObject.GetComponent<Skeleton>());
        }
        else if (type == Type.Bat)
        {
            GameManager.Instance.bats.Remove(gameObject.GetComponent<Bat>());
        }
        else if (type == Type.Dragon)
        {
            GameManager.Instance.dragons.Remove(gameObject.GetComponent<Dragon>());
        }
        Destroy(gameObject);
    }

    public void CheckNextPos(Vector2 nextPos)
    {
        if (GameManager.Instance.monstersNextPos.Contains(nextPos))
        {
            isFull = true;
        }
    }
}
