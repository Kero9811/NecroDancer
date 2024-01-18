using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Skeleton,
    Dragon,
    Bat,
    GreenSlime,
    BlueSlime,
    GoldSlime,
    GoldBat
}

public class Monster : MonoBehaviour
{
    public int currentHp;
    public int maxHp;
    public int dropGold;
    public int damage;
    public Type type;
    public int actionCount;
    public int originCount;
    public bool isFull;
    public bool isAlreadySpotted;

    protected Player target;
    protected SpriteRenderer spriteRenderer;
    protected ChangeColorNearPlayer changeColorNearPlayer;

    private void Awake()
    {
        currentHp = maxHp;
        actionCount = originCount;
    }

    public void Update()
    {
        CheckDistance();
    }

    public void Start()
    {
        target = FindObjectOfType<Player>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = Color.black;
        changeColorNearPlayer = GameManager.Instance.player.GetComponent<ChangeColorNearPlayer>();
    }

    public virtual void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    public virtual void Die()
    {
        GameManager.Instance.player.killCount++;
        GameManager.Instance.player.CheckMultiPoint();

        switch (type)
        {
            case Type.Skeleton:
                GameManager.Instance.skeletons.Remove(gameObject.GetComponent<Skeleton>());
                break;
            case Type.Bat:
                GameManager.Instance.bats.Remove(gameObject.GetComponent<Bat>());
                break;
            case Type.Dragon:
                GameManager.Instance.dragons.Remove(gameObject.GetComponent<Dragon>());
                break;
            case Type.GreenSlime:
                GameManager.Instance.greenSlimes.Remove(gameObject.GetComponent<GreenSlime>());
                break;
            case Type.BlueSlime:
                GameManager.Instance.blueSlimes.Remove(gameObject.GetComponent<BlueSlime>());
                break;
            default:
                return;
        }

        GameObject money = GameManager.Instance.pool.Get(1);
        money.GetComponent<Money>().dropCoin = dropGold;
        money.transform.position = transform.position;

        Destroy(gameObject);
    }

    public void CheckNextPos(Vector2 nextPos)
    {
        if (GameManager.Instance.monstersNextPos.Contains(nextPos))
        {
            isFull = true;
        }
    }

    public virtual void CheckDistance()
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
