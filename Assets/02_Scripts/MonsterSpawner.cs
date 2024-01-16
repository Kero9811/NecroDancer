using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] monsterPrefabs;
    public int monsterCount;

    private void Awake()
    {
        spawnPoints = GetComponentsIntChildeExceptMe(spawnPoints);
    }

    private void Start()
    {
        Invoke("Spawn", .5f);
    }

    void Spawn()
    {
        // ������ ����Ʈ���� ������ �Ǵµ� �ش� ����Ʈ������ ��ó ���� ��ǥ�� ����, ���� �� �̹� ������ ��ǥ�� ������ �ٽ� ���� ��ǥ ����
        for (int i = 0; i < monsterCount; i++)
        {
            int randomNum = Random.Range(0, monsterPrefabs.Length);
            GameObject monster = Instantiate(monsterPrefabs[randomNum], spawnPoints[i].position, Quaternion.identity);
            monster.transform.SetParent(transform.parent);

        }
    }

    private Transform[] GetComponentsIntChildeExceptMe(Transform[] transforms)
    {
        transforms = GetComponentsInChildren<Transform>();
        List<Transform> list = new List<Transform>(transforms);
        list.RemoveAt(0);
        transforms = list.ToArray();

        return transforms;
    }
}
