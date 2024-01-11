using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Rapier : Weapon
{
    [SerializeField] WeaponType weaponType = WeaponType.Rapier;
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
        horizontalRange = 2;
        verticalRange = 1;
        price = 40;
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
        spriteRenderer.sprite = weaponUI.weaponSprites[(int)player.weaponType];
    }
}
