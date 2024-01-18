using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class Node
{
    public Node(bool _isWall, int _x, int _y) { isObstacle = _isWall; x = _x; y = _y; }

    public bool isObstacle;
    public Node ParentNode;

    // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }
}


public class Skeleton : Monster
{
    public Vector2Int bottomLeft, topRight, startPos, targetPos;
    public List<Node> FinalNodeList;

    Animator animator;
    int sizeX, sizeY;
    Node[,] NodeArray;
    Node StartNode, TargetNode, CurNode;
    List<Node> OpenList, ClosedList;


    [SerializeField] float maxHeight;
    [SerializeField] float jumpingDuration;
    [SerializeField] Vector3 jumpStartPos;
    [SerializeField] Vector3 jumpTargetPos;
    public double currentTime = 0;

    new void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();

        GameManager.Instance.skeletons.Add(gameObject.GetComponent<Skeleton>());
    }

    public void Move()
    {
        if (actionCount == 0)
        {
            startPos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            targetPos = new Vector2Int((int)target.nextPos.x, (int)target.nextPos.y);

            PathFinding();

            if (FinalNodeList.Count <= 0)
            {
                return;
            }

            Vector2 nextPos = new Vector2(FinalNodeList[1].x, FinalNodeList[1].y);
            CheckNextPos(nextPos);

            

            if (nextPos == targetPos)
            {
                GameManager.Instance.monstersNextPos.Add(transform.position);
                target.TakeDamage(damage);
            }
            else if (!isFull)
            {
                spriteRenderer.flipX = nextPos.x < targetPos.x;
                GameManager.Instance.monstersNextPos.Add(nextPos);
                Jump(nextPos);
            }
            else { }
            isFull = false;
            actionCount = originCount;
            animator.SetBool("ready", false);
        }
        else
        {
            GameManager.Instance.monstersNextPos.Add(transform.position);
            actionCount--;
            animator.SetBool("ready", true);
        }
    }

    private void PathFinding()
    {
        sizeX = topRight.x - bottomLeft.x + 1;
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f))
                    if (col.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
                        col.gameObject.layer == LayerMask.NameToLayer("Wall"))
                    {
                        isWall = true;
                    }

                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }


        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();


        while (OpenList.Count > 0)
        {
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F < CurNode.F || OpenList[i].F == CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();

                return;
            }
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
    }

    void OpenListAdd(int checkX, int checkY)
    {
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isObstacle && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }

    private void Jump(Vector3 targetPos)
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