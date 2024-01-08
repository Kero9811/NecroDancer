using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SlideMoving : MonoBehaviour
{
    public Vector2Int startPos, targetPos;
    [SerializeField] float slidingDuration;
    [SerializeField] Vector3 slideStartPos;
    [SerializeField] Vector3 slideTargetPos;
    public double currentTime = 0;
    Player target;
    Monster monster;

    Vector2 myPos;
    public LayerMask targetLayer;

    private void Start()
    {
        monster = GetComponent<Monster>();
        target = FindObjectOfType<Player>();
    }

    public void Move()
    {
        if (monster.actionCount == 0)
        {
            int dir = Random.Range(0, 4);
            Vector2 myDir = Vector2.zero;

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
            print(myDir);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, myDir, 1, targetLayer);
            Debug.DrawRay(transform.position, myDir, Color.red);

            if (hit.collider != null)
            {
                monster.actionCount = monster.originCount;
                print(hit.collider.name);
            }
            else
            {
                Slide(myPos);
                monster.actionCount = monster.originCount;
            }
        }
        else
        {
            monster.actionCount--;
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
