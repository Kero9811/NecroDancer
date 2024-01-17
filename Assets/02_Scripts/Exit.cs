using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Exit : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public bool isExecuted;
    public Sprite sprite;
    public float distance;
    public bool isAlreadySpotted;
    Player target;

    private void Start()
    {
        distance = 3.9f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameManager.Instance.player;
        spriteRenderer.color = Color.black;
    }

    private void Update()
    {
        OpenNextStage();
    }

    public void OpenNextStage()
    {
        if (isExecuted)
        {
            spriteRenderer.sprite = sprite;
        }
    }

    public void CheckDistance()
    {
        if (distance >= (GameManager.Instance.player.transform.position - transform.position).magnitude)
        {
            spriteRenderer.color = Color.white;
            isAlreadySpotted = true;
        }
        else if (isAlreadySpotted)
        {
            spriteRenderer.color = target.GetComponent<ChangeColorNearPlayer>().alreadyCheckView;
        }
        else { return; }
    }
}
