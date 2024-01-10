using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dagger : Weapon
{
    WeaponType weaponType = WeaponType.Dagger;
    WeaponUI weaponUI;
    SpriteRenderer spriteRenderer;
    int originDmg;
    int originHorizon;
    int originVertical;
    WeaponType originType;


    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        damage = 1;
        horizontalRange = 1;
        verticalRange = 1;
        originDmg = damage;
        originHorizon = horizontalRange;
        originVertical = verticalRange;
        originType = weaponType;
        weaponUI = FindObjectOfType<WeaponUI>();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            ChanageInfor(player);
            player.damage = originDmg;
            player.horizontalRange = originHorizon;
            player.verticalRange = originVertical;
            player.weaponType = originType;
            weaponUI.ChangeWeapon(originType);
        }
    }

    private void ChanageInfor(Player player)
    {
        originDmg = damage;
        originHorizon = horizontalRange;
        originVertical = verticalRange;
        originType = weaponType;
        damage = player.damage;
        horizontalRange = player.horizontalRange;
        verticalRange = player.verticalRange;
        weaponType = player.weaponType;
        spriteRenderer.sprite = weaponUI.sprites[(int)player.weaponType];
    }
}
