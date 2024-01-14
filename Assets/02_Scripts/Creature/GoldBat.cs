using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBat : Bat
{
    MonsterHpUI monsterHpUI;

    new void Start()
    {
        base.Start();
        monsterHpUI = GetComponentInChildren<MonsterHpUI>();
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
}
