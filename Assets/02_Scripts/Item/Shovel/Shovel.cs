using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShovelGrade
{
    Iron,
    Titanium,
    Diamond
}

public class Shovel : Item
{
    public int digPower;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.digDamage = digPower;
        }
    }
}
