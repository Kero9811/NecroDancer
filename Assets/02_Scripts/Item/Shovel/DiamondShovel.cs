using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiamondShovel : Shovel
{
    new void Start()
    {
        base.Start();
        shovelGrade = ShovelGrade.Diamond;
        digPower = 3;
        price = 100;
        originDmg = digPower;
        originGrade = shovelGrade;
        shovelUI = FindObjectOfType<ShovelUI>();
        text.text = price.ToString();
    }
}
