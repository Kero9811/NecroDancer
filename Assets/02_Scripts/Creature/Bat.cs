using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bat : Monster
{
    public Vector2Int startPos, targetPos;
    [SerializeField] float slidingDuration;
    [SerializeField] Vector3 slideStartPos;
    [SerializeField] Vector3 slideTargetPos;
    public double currentTime = 0;
    Player target;

    Vector2 myPos;
    public LayerMask targetLayer;
    public LayerMask wallLayer;

    private void Start()
    {
        target = FindObjectOfType<Player>();
    }

    public void Move()
    {
        if (actionCount == 0)
        {
            int dir = Random.Range(0, 4);
            Vector2 myDir = Vector2.zero;
            targetPos = new Vector2Int((int)target.nextPos.x, (int)target.nextPos.y);

            switch (dir)
            {
                case 0:
                    myPos = new Vector2Int((int)transform.position.x + 1, (int)transform.position.y);
                    myDir = Vector2.right;
                    break;
                case 1:
                    myPos = new Vector2Int((int)transform.position.x - 1, (int)transform.position.y);
                    myDir = Vector2.left;
                    break;
                case 2:
                    myPos = new Vector2Int((int)transform.position.x, (int)transform.position.y + 1);
                    myDir = Vector2.up;
                    break;
                case 3:
                    myPos = new Vector2Int((int)transform.position.x, (int)transform.position.y - 1);
                    myDir = Vector2.down;
                    break;
            }
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, myDir, 1, targetLayer);
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, myDir, 1, wallLayer);

            if (hit2.collider != null)
            {
                return;
            }
            else if (hit.Length >= 2)
            {
                return;
            }
            else
            {
                if (myPos == targetPos)
                {
                    target.TakeDamage(damage);
                }
                else
                {
                    Slide(myPos);
                }
            }

            actionCount = originCount;
        }
        else
        {
            actionCount--;
        }
    }

    public void Slide(Vector3 targetPos)
    {
        GameManager.Instance.monsterIsMove = true;
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
        GameManager.Instance.monsterIsMove = false;
    }
}
