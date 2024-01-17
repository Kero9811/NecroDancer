using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Player target;
    ChangeColorNearPlayer changeColorNearPlayer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        target = GameManager.Instance.player;
        spriteRenderer.color = Color.black;
        changeColorNearPlayer = target.GetComponent<ChangeColorNearPlayer>();
    }

    private void Update()
    {
        CheckDistance();
    }

    public void CheckDistance()
    {
        // -1칸씩 만큼 어긋나 있음 (임시조치 완)
        if (changeColorNearPlayer.playerCellList.
            Contains(new Vector3Int((int)transform.position.x - 1, (int)transform.position.y - 1, (int)transform.position.z)))
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            Color myColor = spriteRenderer.color;
            myColor.a = 0f;
            spriteRenderer.color = myColor;
        }
    }
}
