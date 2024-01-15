using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public float distance;
    SpriteRenderer spriteRenderer;
    bool isAlreadySpotted;
    Player target;

    private void Awake()
    {
        distance = 3.9f;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = Color.black;
    }

    private void Start()
    {
        target = FindObjectOfType<Player>();
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
