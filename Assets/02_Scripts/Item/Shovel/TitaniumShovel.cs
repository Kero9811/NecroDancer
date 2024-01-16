using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitaniumShovel : Shovel
{
    new void Start()
    {
        base.Start();
        shovelGrade = ShovelGrade.Titanium;
        digPower = 2;
        price = 60;
        originDmg = digPower;
        originGrade = shovelGrade;
        shovelUI = FindObjectOfType<ShovelUI>();
        text.text = price.ToString();
    }
}
