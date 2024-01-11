using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Dagger,
    Sword,
    Whip,
    Rapier
}

public class Weapon : Item
{
    public int damage;
    public int horizontalRange;
    public int verticalRange;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent (out Player player))
        {
            player.damage = damage;
            player.horizontalRange = horizontalRange;
            player.verticalRange = verticalRange;
        }
    }
}
