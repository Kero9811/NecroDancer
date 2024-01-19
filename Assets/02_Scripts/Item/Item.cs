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
    public AudioSource audioSource;
    public BoxCollider2D itemCollider;

    private List<Vector3Int> playerCellList = new List<Vector3Int>();

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
    }

    public void CheckDistance()
    {
        playerCellList = changeColorNearPlayer.playerCellList;

        if (playerCellList.
            Contains(new Vector3Int((int)transform.position.x - 1, (int)transform.position.y - 1, (int)transform.position.z)))
        {
            spriteRenderer.color = Color.white;
            if (!isUseable)
            {
                text.enabled = true;
            }
        }
        else
        {
            Color myColor = spriteRenderer.color;
            myColor.a = 0f;
            spriteRenderer.color = myColor;
            text.enabled = false;
        }
    }
}
