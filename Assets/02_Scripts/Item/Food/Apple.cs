using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Apple : Food
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        maxHeart = 0;
        healPoint = 2;
        price = 10;
        text.text = price.ToString();
        hpUI = FindObjectOfType<ControlHpUI>();
    }
}
