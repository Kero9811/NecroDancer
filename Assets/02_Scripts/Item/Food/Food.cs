using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodList
{
    Apple,
    Cheese,
    Chicken,
    PlusHeart
}

public class Food : Item
{
    public int healPoint;
    public int maxHeart;
    public ControlHpUI hpUI;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (player.coinAmount >= price)
            {
                player.coinAmount -= price;
                player.Heal(healPoint, maxHeart);
                hpUI.ChangeHpUI();
                Destroy(gameObject);
            }
            else { return; }
        }
    }
}