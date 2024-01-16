using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Rapier : Weapon
{
    new void Start()
    {
        base.Start();
        weaponType = WeaponType.Rapier;
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
}
