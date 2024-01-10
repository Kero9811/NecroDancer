using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rapier : Weapon
{
    WeaponType weaponType = WeaponType.Rapier;

    private void Start()
    {
        damage = 1;
        horizontalRange = 2;
        verticalRange = 1;
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
