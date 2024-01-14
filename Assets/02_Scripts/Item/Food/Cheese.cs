using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cheese : Food
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        maxHeart = 0;
        healPoint = 4;
        price = 20;
        text.text = price.ToString();
        hpUI = FindObjectOfType<ControlHpUI>();
    }
}
