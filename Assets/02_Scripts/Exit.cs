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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (isExecuted)
            {
                player.isDone = true;
                player.resultUI.ControlPanel(player.isDead);
                //GameManager.Instance.skeletons.Clear();
                //GameManager.Instance.bats.Clear();
                //GameManager.Instance.blueSlimes.Clear();
                //GameManager.Instance.greenSlimes.Clear();
                //GameManager.Instance.dragons.Clear();
            }
        }
    }
}
