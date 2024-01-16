using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dagger : Weapon
{
    new void Start()
    {
        base.Start();
        weaponType = WeaponType.Dagger;
        damage = 1;
        horizontalRange = 1;
        verticalRange = 1;
        price = 20;
        originDmg = damage;
        originHorizon = horizontalRange;
        originVertical = verticalRange;
        originType = weaponType;
        weaponUI = FindObjectOfType<WeaponUI>();
        text.text = price.ToString();
    }
}
