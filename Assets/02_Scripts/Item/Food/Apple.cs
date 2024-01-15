using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Apple : Food
{
    new void Start()
    {
        base.Start();
        maxHeart = 0;
        healPoint = 2;
        price = 10;
        text.text = price.ToString();
        hpUI = FindObjectOfType<ControlHpUI>();
    }
}
