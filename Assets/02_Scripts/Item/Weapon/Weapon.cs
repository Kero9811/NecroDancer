using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public enum WeaponType
{
    Dagger,
    Sword,
    Whip,
    Rapier
}

public class Weapon : Item
{
    protected int damage;
    protected int horizontalRange;
    protected int verticalRange;
    protected WeaponType weaponType;
    protected WeaponUI weaponUI;
    protected int originDmg;
    protected int originHorizon;
    protected int originVertical;
    protected WeaponType originType;

    public AudioClip[] audioClips;

    new void Start()
    {
        base.Start();
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (isUseable)
            {
                audioSource.clip = audioClips[1];
                audioSource.Play();
                ChanageInfo(player);
                player.damage = originDmg;
                player.horizontalRange = originHorizon;
                player.verticalRange = originVertical;
                player.weaponType = originType;
                weaponUI.ChangeWeapon(originType);
            }
            else if (player.coinAmount >= price)
            {
                audioSource.clip = audioClips[0];
                audioSource.Play();
                player.coinAmount -= price;
                coinText.ChangeCoinUI();
                ChanageInfo(player);
                player.damage = originDmg;
                player.horizontalRange = originHorizon;
                player.verticalRange = originVertical;
                player.weaponType = originType;
                weaponUI.ChangeWeapon(originType);
                isUseable = true;
                text.enabled = false;
            }
            else { return; }
        }
    }

    protected void ChanageInfo(Player player)
    {
        originDmg = damage;
        originHorizon = horizontalRange;
        originVertical = verticalRange;
        originType = weaponType;
        damage = player.damage;
        horizontalRange = player.horizontalRange;
        verticalRange = player.verticalRange;
        weaponType = player.weaponType;
        spriteRenderer.sprite = weaponUI.weaponSprites[(int)player.weaponType];
    }
}
