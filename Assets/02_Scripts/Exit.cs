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
    Player target;
    ChangeColorNearPlayer changeColorNearPlayer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameManager.Instance.player;
        spriteRenderer.color = Color.black;
        changeColorNearPlayer = target.GetComponent<ChangeColorNearPlayer>();
    }

    private void Update()
    {
        OpenNextStage();
        CheckDistance();
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
        // -1ĭ�� ��ŭ ��߳� ���� (�ӽ���ġ ��)
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
