using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IronShovel : Shovel
{
    ShovelGrade shovelGrade = ShovelGrade.Iron;
    [SerializeField] ShovelUI shovelUI;
    int originDmg;
    ShovelGrade originGrade;


    new void Start()
    {
        base.Start();
        digPower = 1;
        price = 20;
        originDmg = digPower;
        originGrade = shovelGrade;
        shovelUI = FindObjectOfType<ShovelUI>();
        text.text = price.ToString();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (isUseable)
            {
                ChanageInfor(player);
                player.digDamage = originDmg;
                player.shovelGrade = originGrade;
                shovelUI.ChangeShovel(originGrade);
            }
            else if (player.coinAmount >= price)
            {
                player.coinAmount -= price;
                coinText.ChangeCoinUI();
                ChanageInfor(player);
                player.digDamage = originDmg;
                player.shovelGrade = originGrade;
                shovelUI.ChangeShovel(originGrade);
                isUseable = true;
                text.enabled = false;
            }
            else { return; }
        }
    }

    private void ChanageInfor(Player player)
    {
        originDmg = digPower;
        originGrade = shovelGrade;
        digPower = player.digDamage;
        shovelGrade = player.shovelGrade;
        spriteRenderer.sprite = shovelUI.shovelSprites[(int)player.shovelGrade];
    }
}
