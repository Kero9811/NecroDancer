using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cheese : Food
{
    new void Start()
    {
        base.Start();
        maxHeart = 0;
        healPoint = 4;
        price = 20;
        text.text = price.ToString();
        hpUI = FindObjectOfType<ControlHpUI>();
    }
}
