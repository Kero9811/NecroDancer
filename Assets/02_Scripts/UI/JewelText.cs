using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JewelText : MonoBehaviour
{
    TextMeshProUGUI text;
    int jewelText;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        jewelText = GameManager.Instance.player.jewelAmount;
    }

    private void Update()
    {
        jewelText = GameManager.Instance.player.jewelAmount;
        text.text = $"x {jewelText}";
    }
}
