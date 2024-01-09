using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    AstarMove,
    SlideMove,
    BatMove
}

public class Monster : MonoBehaviour
{
    [SerializeField] int currentHp = 0;
    [SerializeField] int maxHp = 2;

    public int damage;
    public Type type;
    public int actionCount;
    public int originCount;

    private void Awake()
    {
        currentHp = maxHp;
        actionCount = originCount;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if(currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.player.currentCoinPoint++;
        GameManager.Instance.player.CheckMultiPoint();

        if (type == Type.AstarMove)
        {
            GameManager.Instance.aStarMovings.Remove(gameObject.GetComponent<AStarMoving>());
        }
        else if (type == Type.BatMove)
        {
            GameManager.Instance.slideMovings.Remove(gameObject.GetComponent<BatMove>());
        }
        Destroy(gameObject);
    }
}
