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
    public AudioSource audioSource;
    public BoxCollider2D itemCollider;

    public void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        itemCollider = GetComponent<BoxCollider2D>();
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
            isAlreadySpotted = true;
            if (!isUseable)
            {
                text.enabled = true;
            }
        }
        else if (isAlreadySpotted)
        {
            spriteRenderer.color = changeColorNearPlayer.alreadyCheckView;
            text.enabled = false;
        }
        else { return; }
    }
}
