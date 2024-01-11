using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Whip : Weapon
{
    WeaponType weaponType = WeaponType.Whip;
    WeaponUI weaponUI;
    SpriteRenderer spriteRenderer;
    int originDmg;
    int originHorizon;
    int originVertical;
    [SerializeField] WeaponType originType;
    TextMeshProUGUI text;


    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        damage = 1;
        horizontalRange = 1;
        verticalRange = 5;
        price = 60;
        originDmg = damage;
        originHorizon = horizontalRange;
        originVertical = verticalRange;
        originType = weaponType;
        weaponUI = FindObjectOfType<WeaponUI>();
        text.text = price.ToString();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (isUseable)
            {
                ChanageInfor(player);
                player.damage = originDmg;
                player.horizontalRange = originHorizon;
                player.verticalRange = originVertical;
                player.weaponType = originType;
                weaponUI.ChangeWeapon(originType);
            }
            else if (player.coinAmount >= price)
            {
                player.coinAmount -= price;
                ChanageInfor(player);
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
