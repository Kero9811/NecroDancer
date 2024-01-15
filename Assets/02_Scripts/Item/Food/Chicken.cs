using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chicken : Food
{
    new void Start()
    {
        base.Start();
        maxHeart = 0;
        healPoint = 6;
        price = 35;
        text.text = price.ToString();
        hpUI = FindObjectOfType<ControlHpUI>();
    }
}
