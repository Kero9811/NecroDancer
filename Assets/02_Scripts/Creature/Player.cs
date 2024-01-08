using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Player : MonoBehaviour
{
    SpriteRenderer headRenderer;
    SpriteRenderer bodyRenderer;

    [SerializeField] int currentHp;
    [SerializeField] int maxHp;
    [SerializeField] int damage;
    [SerializeField] int defense;
    public int currentCoinPoint = 1;
    [SerializeField] int maxCoinPoint = 4;
    public int coinAmount;
    public int jewelAmount;

    [SerializeField] float lastInputTime = 0;
    [SerializeField] float inputCoolTime = .2f;

    [SerializeField] float maxHeight;
    [SerializeField] float jumpingDuration;
    [SerializeField] bool isMoving;
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 targetPos;
    public Vector2 nextPos;
    public bool isCorrect;
    private bool refuseInput;

    public LayerMask targetLayer;

    private void Awake()
    {
        headRenderer = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        bodyRenderer = transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentHp = maxHp;
    }

    private void Update()
    {
        if (Time.time - lastInputTime >= inputCoolTime)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !isMoving)
            {
                GameManager.Instance.timingManager.CheckTiming();

                CheckRhythm();

                if (!refuseInput)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1, targetLayer);

                    if (hit.collider != null)
                    {
                        Attack(hit.collider, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z));
                        return;
                    }

                    Vector2 nextPos = new Vector2(transform.position.x, transform.position.y + 1);
                    Jump(nextPos);
                    this.nextPos = nextPos;
                    GameManager.Instance.playerMove();
                    lastInputTime = Time.time;
                }

                refuseInput = false;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && !isMoving)
            {
                GameManager.Instance.timingManager.CheckTiming();

                CheckRhythm();

                if (!refuseInput)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1, targetLayer);

                    if (hit.collider != null)
                    {
                        Attack(hit.collider, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z));
                        return;
                    }

                    Vector2 nextPos = new Vector2(transform.position.x, transform.position.y - 1);
                    Jump(nextPos);
                    this.nextPos = nextPos;
                    GameManager.Instance.playerMove();
                    lastInputTime = Time.time;
                }

                refuseInput = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && !isMoving)
            {
                GameManager.Instance.timingManager.CheckTiming();

                CheckRhythm();

                headRenderer.flipX = true;
                bodyRenderer.flipX = true;

                if (!refuseInput)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 1, targetLayer);

                    if (hit.collider != null)
                    {
                        Attack(hit.collider, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z));
                        return;
                    }

                    Vector2 nextPos = new Vector2(transform.position.x - 1, transform.position.y);
                    Jump(nextPos);
                    this.nextPos = nextPos;
                    GameManager.Instance.playerMove();
                    lastInputTime = Time.time;
                }

                refuseInput = false;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && !isMoving)
            {
                GameManager.Instance.timingManager.CheckTiming();

                CheckRhythm();

                headRenderer.flipX = false;
                bodyRenderer.flipX = false;

                if (!refuseInput)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1, targetLayer);

                    if (hit.collider != null)
                    {
                        Attack(hit.collider, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z));
                        return;
                    }

                    Vector2 nextPos = new Vector2(transform.position.x + 1, transform.position.y);
                    Jump(nextPos);
                    this.nextPos = nextPos;
                    GameManager.Instance.playerMove();
                    lastInputTime = Time.time;
                }

                refuseInput = false;
            }
            else
            {
                return;
            }
        }
    }

    private void CheckRhythm()
    {
        if (!isCorrect || GameManager.Instance.monsterIsMove)
        {
            GameManager.Instance.shakeCamera.StartShake();
            currentCoinPoint = 1;
            refuseInput = true;
        }
    }

    private void Attack(Collider2D collider, Vector3 wallPos)
    {
        GameManager.Instance.shakeCamera.StartShake();
        if (collider.tag == "Wall")
        {
            if (collider.TryGetComponent(out Tilemap tileMap))
            {
                Vector3Int cellPos = tileMap.WorldToCell(wallPos);
                tileMap.SetTile(cellPos, null);
            }
        }
        else if (collider.TryGetComponent(out Monster monster))
        {
            monster.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    public void TakeDamage(int damage)
    {
        GameManager.Instance.shakeCamera.StartShake();

        currentHp -= damage;

        if (currentHp <= 0)
        {
            currentHp = 0;
            //Die();
        }
    }

    public void Jump(Vector3 targetPos)
    {
        startPos = transform.position;
        this.targetPos = targetPos;
        isMoving = true;

        _ = StartCoroutine(JumpingCoroutine());
    }

    IEnumerator JumpingCoroutine()
    {
        float startTime = Time.time;
        float endTime = startTime + jumpingDuration;

        while (Time.time < endTime)
        {
            float currentTime = Time.time - startTime;
            float durationAmount = currentTime / jumpingDuration;
            float currentHeight;
            float currentDegreeLerp = Mathf.Lerp(0, 180, durationAmount);

            currentHeight = Mathf.Sin(currentDegreeLerp * Mathf.Deg2Rad) * maxHeight;

            transform.position = Vector2.Lerp(startPos, targetPos, durationAmount) + new Vector2(0, currentHeight);

            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }

    internal void CheckMultiPoint()
    {
        if (currentCoinPoint > maxCoinPoint)
        {
            currentCoinPoint = maxCoinPoint;
        }
    }
}
