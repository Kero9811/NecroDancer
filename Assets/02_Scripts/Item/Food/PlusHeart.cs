using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlusHeart : Food
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        maxHeart = 2;
        healPoint = 2;
        price = 40;
        text.text = price.ToString();
        hpUI = FindObjectOfType<ControlHpUI>();
    }
}

