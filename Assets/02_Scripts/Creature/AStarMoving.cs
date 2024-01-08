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


public class AStarMoving : MonoBehaviour
{
    public Vector2Int bottomLeft, topRight, startPos, targetPos;
    public List<Node> FinalNodeList;

    Player target;

    int sizeX, sizeY;
    Node[,] NodeArray;
    Node StartNode, TargetNode, CurNode;
    List<Node> OpenList, ClosedList;


    [SerializeField] float maxHeight;
    [SerializeField] float jumpingDuration;
    [SerializeField] Vector3 jumpStartPos;
    [SerializeField] Vector3 jumpTargetPos;
    public double currentTime = 0;

    Monster monster;


    private void Start()
    {
        monster = GetComponent<Monster>();
        target = FindObjectOfType<Player>();
    }

    public void Move()  // 특수 액션에 들어가지 않았다면 실행, 들어갔다면 return
    {
        if (monster.actionCount == 0)
        {
            startPos = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            targetPos = new Vector2Int((int)target.nextPos.x, (int)target.nextPos.y);

            PathFinding();

            if (FinalNodeList.Count <= 0)
            {
                return;
            }

            Vector2 nextPos = new Vector2(FinalNodeList[1].x, FinalNodeList[1].y);
            if (nextPos == targetPos)
            {
                target.TakeDamage(monster.damage);
            }
            else
            {
                Jump(nextPos);
            }

            monster.actionCount = monster.originCount;
        }
        else
        {
            monster.actionCount--;
        }
    }


    public void PathFinding()
    {
        // NodeArray의 크기 정해주고, isWall, x, y 대입
        sizeX = topRight.x - bottomLeft.x + 1;
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f))
                    if (col.gameObject.layer == LayerMask.NameToLayer("Contactable")) isWall = true;

                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }


        // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();


        while (OpenList.Count > 0)
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                //if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];
                if (OpenList[i].F < CurNode.F || OpenList[i].F == CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            // 마지막
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
            // ↑ → ↓ ←
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isObstacle && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }

    public void Jump(Vector3 targetPos)
    {
        GameManager.Instance.monsterIsMove = true;
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
        GameManager.Instance.monsterIsMove = false;
    }
}