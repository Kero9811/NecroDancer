using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IronShovel : Shovel
{
    new void Start()
    {
        base.Start();
        shovelGrade = ShovelGrade.Iron;
        digPower = 1;
        price = 20;
        originDmg = digPower;
        originGrade = shovelGrade;
        shovelUI = FindObjectOfType<ShovelUI>();
        text.text = price.ToString();
    }
}
