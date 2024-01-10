using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    WeaponType weaponType = WeaponType.Sword;

    private void Start()
    {
        damage = 1;
        horizontalRange = 1;
        verticalRange = 3;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.damage = damage;
            player.horizontalRange = horizontalRange;
            player.verticalRange = verticalRange;
            player.weaponType = weaponType;
        }
    }
}
