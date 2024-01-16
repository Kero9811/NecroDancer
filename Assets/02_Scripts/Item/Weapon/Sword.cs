using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sword : Weapon
{
    new void Start()
    {
        base.Start();
        weaponType = WeaponType.Sword;
        damage = 1;
        horizontalRange = 1;
        verticalRange = 3;
        price = 40;
        originDmg = damage;
        originHorizon = horizontalRange;
        originVertical = verticalRange;
        originType = weaponType;
        weaponUI = FindObjectOfType<WeaponUI>();
        text.text = price.ToString();
    }
}
