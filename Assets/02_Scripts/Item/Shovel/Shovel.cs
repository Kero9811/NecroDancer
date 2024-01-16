using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShovelGrade
{
    Iron,
    Titanium,
    Diamond
}

public class Shovel : Item
{
    protected int digPower;
    protected ShovelGrade shovelGrade;
    protected ShovelUI shovelUI;
    protected int originDmg;
    protected ShovelGrade originGrade;

    public AudioClip[] audioClips;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (isUseable)
            {
                audioSource.clip = audioClips[1];
                audioSource.Play();
                ChanageInfor(player);
                player.digDamage = originDmg;
                player.shovelGrade = originGrade;
                shovelUI.ChangeShovel(originGrade);
            }
            else if (player.coinAmount >= price)
            {
                audioSource.clip = audioClips[0];
                audioSource.Play();
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

    protected void ChanageInfor(Player player)
    {
        originDmg = digPower;
        originGrade = shovelGrade;
        digPower = player.digDamage;
        shovelGrade = player.shovelGrade;
        spriteRenderer.sprite = shovelUI.shovelSprites[(int)player.shovelGrade];
    }
}
