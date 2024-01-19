using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Exit : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public bool isExecuted;
    public Sprite sprite;
    Player target;
    ChangeColorNearPlayer changeColorNearPlayer;
    public bool isAlreadySpotted;
    Color myColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameManager.Instance.player;
        changeColorNearPlayer = target.GetComponent<ChangeColorNearPlayer>();
        myColor = spriteRenderer.color;
        myColor.a = 0f;
        spriteRenderer.color = myColor;
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
        if (changeColorNearPlayer.playerCellList.
            Contains(new Vector3Int((int)transform.position.x - 1, (int)transform.position.y - 1, (int)transform.position.z)))
        {
            spriteRenderer.color = Color.white;
            isAlreadySpotted = true;
        }
        else
        {
            if (isAlreadySpotted)
            {
                spriteRenderer.color = changeColorNearPlayer.alreadyCheckView;
            }
            else { return; }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (isExecuted)
            {
                player.isDone = true;
                PlayerInfo.Instance.DownloadInfo();
                if (PlayerInfo.Instance.currentStageIdx == 4)
                {
                    PlayerInfo.Instance.currentStageIdx = 1;
                    player.resultUI.ControlPanel(player.isDead);
                }
                else
                {
                    SceneLoader.Instance.LoadGameScene(SceneLoader.Instance.stages[PlayerInfo.Instance.currentStageIdx]);
                }
            }
        }
    }
}
