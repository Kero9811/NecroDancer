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

    new void Start()
    {
        base.Start();
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (player.coinAmount >= price)
            {
                player.coinAmount -= price;
                coinText.ChangeCoinUI();
                player.Heal(healPoint, maxHeart);
                hpUI.ChangeHpUI();
                spriteRenderer.enabled = false;
                itemCollider.enabled = false;
                text.text = "";
                audioSource.Play();

                Invoke("DestroyObject", 1.5f);
            }
            else { return; }
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
