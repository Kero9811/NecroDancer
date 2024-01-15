using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int price;
    public bool isUseable = false;
    public CoinText coinText;
    public ChangeColorNearPlayer changeColorNearPlayer;
    public SpriteRenderer spriteRenderer;
    public TextMeshProUGUI text;
    public float distance;
    public bool isAlreadySpotted;

    public void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        CheckDistance();
    }

    public void Start()
    {
        coinText = FindObjectOfType<CoinText>();
        changeColorNearPlayer = GameManager.Instance.player.GetComponent<ChangeColorNearPlayer>();
        spriteRenderer.color = Color.black;
        text.enabled = false;
        distance = 3.9f;
    }

    public void CheckDistance()
    {
        if (distance >= (GameManager.Instance.player.transform.position - transform.position).magnitude)
        {
            spriteRenderer.color = Color.white;
            text.enabled = true;
            isAlreadySpotted = true;
        }
        else if (isAlreadySpotted)
        {
            spriteRenderer.color = changeColorNearPlayer.alreadyCheckView;
            text.enabled = false;
        }
        else { return; }
    }
}
