using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Up,
    Down
}

public class BlueSlime : Monster
{
    MonsterHpUI monsterHpUI;
    State state;

    [SerializeField] float maxHeight;
    [SerializeField] float jumpingDuration;
    [SerializeField] Vector3 jumpStartPos;
    [SerializeField] Vector3 jumpTargetPos;
    public Vector2Int targetPos;

    new void Start()
    {
        base.Start();
        originCount = 1;
        actionCount = originCount;
        monsterHpUI = GetComponentInChildren<MonsterHpUI>();
        state = State.Up;

        GameManager.Instance.blueSlimes.Add(gameObject.GetComponent<BlueSlime>());
    }

    public void Move()
    {
        if (actionCount == 0)
        {
            targetPos = new Vector2Int((int)target.nextPos.x, (int)target.nextPos.y);

            if (state == State.Up)
            {
                Vector2 nextPos = new Vector2(transform.position.x, transform.position.y - 1);
                CheckNextPos(nextPos);
                if (nextPos == targetPos)
                {
                    GameManager.Instance.monstersNextPos.Add(transform.position);
                    target.TakeDamage(damage);
                }
                else if (!isFull)
                {
                    GameManager.Instance.monstersNextPos.Add(nextPos);
                    Jump(nextPos);
                    state = State.Down;
                }
                else { }
                isFull = false;

                actionCount = originCount;
            }
            else
            {
                Vector2 nextPos = new Vector2(transform.position.x, transform.position.y + 1);
                CheckNextPos(nextPos);
                if (nextPos == targetPos)
                {
                    GameManager.Instance.monstersNextPos.Add(transform.position);
                    target.TakeDamage(damage);
                }
                else if (!isFull)
                {
                    GameManager.Instance.monstersNextPos.Add(nextPos);
                    Jump(nextPos);
                    state = State.Up;
                }
                else { }
                isFull = false;

                actionCount = originCount;
            }
        }
        else
        {
            GameManager.Instance.monstersNextPos.Add(transform.position);
            actionCount--;
        }
    }

    public override void TakeDamage(int damage)
    {
        currentHp -= damage;
        monsterHpUI.ChangeHpUI();

        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }


    public void Jump(Vector3 targetPos)
    {
        jumpStartPos = transform.position;
        jumpTargetPos = targetPos;

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

            transform.position = Vector2.Lerp(jumpStartPos, jumpTargetPos, durationAmount) + new Vector2(0, currentHeight);

            yield return null;
        }

        transform.position = jumpTargetPos;
    }
}
