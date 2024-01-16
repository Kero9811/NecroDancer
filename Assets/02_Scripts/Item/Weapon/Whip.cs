using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Whip : Weapon
{
    new void Start()
    {
        base.Start();
        weaponType = WeaponType.Whip;
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
}
