using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bat : Monster
{
    public Vector2Int targetPos;
    [SerializeField] float slidingDuration;
    [SerializeField] Vector3 slideStartPos;
    [SerializeField] Vector3 slideTargetPos;
    public double currentTime = 0;

    Vector2 myPos;
    public LayerMask targetLayer;
    public LayerMask wallLayer;

    new void Start()
    {
        base.Start();

        GameManager.Instance.bats.Add(gameObject.GetComponent<Bat>());
    }

    public void Move()
    {
        if (actionCount == 0)
        {
            Vector2[] directions = {Vector2.right, Vector2.left, Vector2.up, Vector2.down};
            bool isTrapped = true;
            targetPos = new Vector2Int((int)target.nextPos.x, (int)target.nextPos.y);

            foreach(var direction in directions)
            {
                if (!IsBlockedInDirection(direction))
                {
                    isTrapped = false;
                    break;
                }
            }

            if (!isTrapped)
            {
                Vector2 randomDirection;

                do
                {
                    randomDirection = directions[Random.Range(0, directions.Length)];
                } while (IsBlockedInDirection(randomDirection));

                if (randomDirection == Vector2.right)
                {
                    spriteRenderer.flipX = false;
                    myPos = new Vector2Int((int)transform.position.x + 1, (int)transform.position.y);
                }
                else if (randomDirection == Vector2.left)
                {
                    spriteRenderer.flipX = true;
                    myPos = new Vector2Int((int)transform.position.x - 1, (int)transform.position.y);
                }
                else if (randomDirection == Vector2.up)
                {
                    myPos = new Vector2Int((int)transform.position.x, (int)transform.position.y + 1);
                }
                else if (randomDirection == Vector2.down)
                {
                    myPos = new Vector2Int((int)transform.position.x, (int)transform.position.y - 1);
                }
                else { }

                CheckNextPos(myPos);


                if (myPos == targetPos)
                {
                    target.TakeDamage(damage);
                    GameManager.Instance.monstersNextPos.Add(transform.position);
                }
                else if (!isFull)
                {
                    GameManager.Instance.monstersNextPos.Add(myPos);
                    Slide(myPos);
                }
                else { }
            }

            actionCount = originCount;
        }
        else
        {
            GameManager.Instance.monstersNextPos.Add(transform.position);
            actionCount--;
        }
    }

    private bool IsBlockedInDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1, wallLayer);
        RaycastHit2D[] hit2 = Physics2D.RaycastAll(transform.position, direction, 1, targetLayer);

        return hit.collider != null || hit2.Length >= 2;
    }

    private void Slide(Vector3 targetPos)
    {
        slideStartPos = transform.position;
        slideTargetPos = targetPos;

        _ = StartCoroutine(SlidingCoroutine());
    }

    IEnumerator SlidingCoroutine()
    {
        float startTime = Time.time;
        float endTime = startTime + slidingDuration;

        while (Time.time < endTime)
        {
            float currentTime = Time.time - startTime;
            float durationAmount = currentTime / slidingDuration;

            transform.position = Vector2.Lerp(slideStartPos, slideTargetPos, durationAmount);

            yield return null;
        }

        transform.position = slideTargetPos;
    }
}
