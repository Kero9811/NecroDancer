using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


public class Player : MonoBehaviour
{
    SpriteRenderer headRenderer;
    SpriteRenderer bodyRenderer;

    public int currentHp;
    public int maxHp;
    public int damage;
    public int digDamage;
    public int defense;
    public int currentCoinPoint;
    public int killCount = 0;
    [SerializeField] int maxCoinPoint = 4;
    public int coinAmount;
    public int jewelAmount;

    [SerializeField] float lastInputTime = 0;
    [SerializeField] float inputCoolTime = .2f;

    [SerializeField] float maxHeight;
    [SerializeField] float jumpingDuration;
    public bool isMoving;
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 targetPos;
    public Vector2 nextPos;
    public bool isCorrect;
    private bool refuseInput;
    public AudioClip[] audioClips;
    AudioSource audioSource;

    public WeaponType weaponType;
    public ShovelGrade shovelGrade;

    public LayerMask targetLayer;
    public LayerMask wallLayer;

    public int sizeX, sizeY;
    public int horizontalRange;
    public int verticalRange;

    Vector2Int bottomLeft;
    Vector2Int topRight;
    bool isAttack;
    public bool isMiss;

    ChangeColorNearPlayer changeColorNearPlayer;
    public Item[] items;
    ShopKeeper shopKeeper;

    private void Awake()
    {
        headRenderer = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        bodyRenderer = transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>();
        changeColorNearPlayer = GetComponent<ChangeColorNearPlayer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        currentHp = maxHp;
        weaponType = WeaponType.Dagger;
        shovelGrade = ShovelGrade.Iron;
        items = FindObjectsOfType<Item>();
        shopKeeper = FindObjectOfType<ShopKeeper>();
        currentCoinPoint = 1;
    }

    private void Update()
    {
        if (Time.time - lastInputTime >= inputCoolTime)
        {
            if (isMiss)
            {
                lastInputTime = Time.time -.1f;
                isMiss = false;
                return;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && !isMoving)
            {
                GameManager.Instance.timingManager.CheckTiming();

                CheckRhythm();

                Vector2 nextPos = new Vector2(transform.position.x, transform.position.y + 1);

                if (!refuseInput)
                {
                    RaycastHit2D wall = Physics2D.Raycast(transform.position, Vector2.up, 1, wallLayer);

                    if (wall.collider != null)
                    {
                        if (wall.collider.TryGetComponent(out WallInfo wallInfo))
                        {
                            if (digDamage >= wallInfo.wallStrength)
                            {
                                DigWall(wall.collider, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z));
                                return;
                            }
                            else
                            {
                                audioSource.clip = audioClips[5];
                                audioSource.Play();
                                GameManager.Instance.shakeCamera.StartShake(5f, .1f);
                                currentCoinPoint = 1;
                                refuseInput = true;
                                return;
                            }
                        }
                        else if (wall.collider.TryGetComponent(out ShopKeeper shopKeeper))
                        {
                            GameManager.Instance.shakeCamera.StartShake(5f, .1f);
                            currentCoinPoint = 1;
                            refuseInput = true;
                            return;
                        }
                    }

                    bottomLeft = new Vector2Int((int)nextPos.x - (verticalRange / 2), (int)nextPos.y);
                    topRight = new Vector2Int((int)nextPos.x + (verticalRange / 2), (int)nextPos.y + (horizontalRange / 2));

                    RangeAttack(bottomLeft, topRight);
                    if (isAttack)
                    {
                        SpawnEffect(weaponType, nextPos, new Vector3(0, 0, 90));
                        isAttack = false;
                        return;
                    }

                    Jump(nextPos);
                    this.nextPos = nextPos;
                    GameManager.Instance.playerMove();
                    changeColorNearPlayer.ChangeTileColor(nextPos);
                    FindItems();
                    FindMerchant();
                    lastInputTime = Time.time;
                }

                refuseInput = false;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && !isMoving)
            {
                GameManager.Instance.timingManager.CheckTiming();

                CheckRhythm();

                Vector2 nextPos = new Vector2(transform.position.x, transform.position.y - 1);

                if (!refuseInput)
                {
                    RaycastHit2D wall = Physics2D.Raycast(transform.position, Vector2.down, 1, wallLayer);

                    if (wall.collider != null)
                    {
                        if (wall.collider.TryGetComponent(out WallInfo wallInfo))
                        {
                            if (digDamage >= wallInfo.wallStrength)
                            {
                                DigWall(wall.collider, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z));
                                return;
                            }
                            else
                            {
                                audioSource.clip = audioClips[5];
                                audioSource.Play();
                                GameManager.Instance.shakeCamera.StartShake(5f, .1f);
                                currentCoinPoint = 1;
                                refuseInput = true;
                                return;
                            }
                        }
                        else if (wall.collider.TryGetComponent(out ShopKeeper shopKeeper))
                        {
                            GameManager.Instance.shakeCamera.StartShake(5f, .1f);
                            currentCoinPoint = 1;
                            refuseInput = true;
                            return;
                        }
                    }

                    bottomLeft = new Vector2Int((int)nextPos.x - (verticalRange / 2), (int)nextPos.y - (horizontalRange / 2));
                    topRight = new Vector2Int((int)nextPos.x + (verticalRange / 2), (int)nextPos.y);

                    RangeAttack(bottomLeft, topRight);
                    if (isAttack)
                    {
                        SpawnEffect(weaponType, nextPos, new Vector3(0, 0, -90));
                        isAttack = false;
                        return;
                    }

                    Jump(nextPos);
                    this.nextPos = nextPos;
                    GameManager.Instance.playerMove();
                    changeColorNearPlayer.ChangeTileColor(nextPos);
                    FindItems();
                    FindMerchant();
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

                Vector2 nextPos = new Vector2(transform.position.x - 1, transform.position.y);

                if (!refuseInput)
                {
                    RaycastHit2D wall = Physics2D.Raycast(transform.position, Vector2.left, 1, wallLayer);

                    if (wall.collider != null)
                    {
                        if (wall.collider.TryGetComponent(out WallInfo wallInfo))
                        {
                            if (digDamage >= wallInfo.wallStrength)
                            {
                                DigWall(wall.collider, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z));
                                return;
                            }
                            else
                            {
                                audioSource.clip = audioClips[5];
                                audioSource.Play();
                                GameManager.Instance.shakeCamera.StartShake(5f, .1f);
                                currentCoinPoint = 1;
                                refuseInput = true;
                                return;
                            }
                        }
                        else if (wall.collider.TryGetComponent(out ShopKeeper shopKeeper))
                        {
                            GameManager.Instance.shakeCamera.StartShake(5f, .1f);
                            currentCoinPoint = 1;
                            refuseInput = true;
                            return;
                        }
                    }

                    bottomLeft = new Vector2Int((int)nextPos.x - (horizontalRange / 2), (int)nextPos.y - (verticalRange / 2));
                    topRight = new Vector2Int((int)nextPos.x, (int)nextPos.y + (verticalRange / 2));

                    RangeAttack(bottomLeft, topRight);
                    if (isAttack)
                    {
                        SpawnEffect(weaponType, nextPos, new Vector3(0, 180, 0));
                        isAttack = false;
                        return;
                    }

                    Jump(nextPos);
                    this.nextPos = nextPos;
                    GameManager.Instance.playerMove();
                    changeColorNearPlayer.ChangeTileColor(nextPos);
                    FindItems();
                    FindMerchant();
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

                Vector2 nextPos = new Vector2(transform.position.x + 1, transform.position.y);

                if (!refuseInput)
                {
                    RaycastHit2D wall = Physics2D.Raycast(transform.position, Vector2.right, 1, wallLayer);

                    if (wall.collider != null)
                    {
                        if (wall.collider.TryGetComponent(out WallInfo wallInfo))
                        {
                            if (digDamage >= wallInfo.wallStrength)
                            {
                                DigWall(wall.collider, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z));
                                return;
                            }
                            else
                            {
                                audioSource.clip = audioClips[5];
                                audioSource.Play();
                                GameManager.Instance.shakeCamera.StartShake(5f, .1f);
                                currentCoinPoint = 1;
                                refuseInput = true;
                                return;
                            }
                        }
                        else if (wall.collider.TryGetComponent(out ShopKeeper shopKeeper))
                        {
                            GameManager.Instance.shakeCamera.StartShake(5f, .1f);
                            currentCoinPoint = 1;
                            refuseInput = true;
                            return;
                        }
                    }

                    bottomLeft = new Vector2Int((int)nextPos.x, (int)nextPos.y - (verticalRange / 2));
                    topRight = new Vector2Int((int)nextPos.x + (horizontalRange / 2), (int)nextPos.y + (verticalRange / 2));

                    RangeAttack(bottomLeft, topRight);
                    if (isAttack)
                    {
                        SpawnEffect(weaponType,nextPos, new Vector3(0, 0, 0));
                        isAttack = false;
                        return;
                    }

                    Jump(nextPos);
                    this.nextPos = nextPos;
                    GameManager.Instance.playerMove();
                    changeColorNearPlayer.ChangeTileColor(nextPos);
                    FindItems();
                    FindMerchant();
                    lastInputTime = Time.time;
                }

                refuseInput = false;
            }
            else { return; }
        }
    }
    private void CheckRhythm()
    {
        if (!isCorrect)
        {
            audioSource.clip = audioClips[2];
            audioSource.Play();
            GameManager.Instance.shakeCamera.StartShake(5f, .1f);
            currentCoinPoint = 1;
            refuseInput = true;
        }
    }


    public void RangeAttack(Vector2Int topRight, Vector2Int bottomLeft)
    {
        Collider2D[] colliders = Physics2D.OverlapAreaAll(bottomLeft, topRight);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Monster monster))
            {
                audioSource.clip = audioClips[0];
                audioSource.Play();
                monster.TakeDamage(damage);
                isAttack = true;
            }
        }
    }

    private void SpawnEffect(WeaponType type, Vector2 nextPos, Vector3 rotateValue)
    {
        GameObject effect = GameManager.Instance.pool.Get((int)type + 2);
        effect.transform.position = nextPos;
        effect.transform.rotation = Quaternion.identity;
        effect.transform.Rotate(rotateValue);
    }

    private void DigWall(Collider2D collider, Vector3 wallPos)
    {
        GameManager.Instance.shakeCamera.StartShake(1f, .1f);
        if (collider.TryGetComponent(out Tilemap tileMap))
        {
            audioSource.clip = audioClips[4];
            audioSource.Play();
            Vector3Int cellPos = tileMap.WorldToCell(wallPos);
            tileMap.SetTile(cellPos, null);
        }
        else { return; }
    }

    public void TakeDamage(int damage)
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();

        GameManager.Instance.shakeCamera.StartShake(5f, .1f);

        currentHp -= damage;

        GameManager.Instance.controlHpUI.ChangeHpUI();

        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    public void Die()
    {
        audioSource.clip = audioClips[3];
        audioSource.Play();

        // »ç¸ÁÃ³¸®
    }

    public void Heal(int healPoint, int maxHeart)
    {
        maxHp += maxHeart;
        currentHp += healPoint;

        if (currentHp >= maxHp)
        {
            currentHp = maxHp;
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
        if (killCount >= 3)
        {
            currentCoinPoint++;
            killCount = 0;
            if (currentCoinPoint > maxCoinPoint)
            {
                currentCoinPoint = maxCoinPoint;
            }
        }
    }

    private void FindItems()
    {
        foreach (var item in items)
        {
            if (item != null)
            {
                item.CheckDistance();
            }
            else { return; }
        }
    }

    private void FindMerchant()
    {
        shopKeeper.CheckDistance();
    }
}
