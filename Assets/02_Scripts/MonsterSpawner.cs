using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] monsterPrefabs;
    public GameObject[] eliteMonsterPrefabs;
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
        // 랜덤한 포인트에서 스폰이 되는데 해당 포인트에서도 근처 랜덤 좌표로 스폰, 스폰 시 이미 스폰된 좌표가 있으면 다시 랜덤 좌표 선택
        for (int i = 0; i < monsterCount; i++)
        {
            int randomNum = Random.Range(0, monsterPrefabs.Length);
            GameObject monster = Instantiate(monsterPrefabs[randomNum], spawnPoints[i].position, Quaternion.identity);
            monster.transform.SetParent(transform.parent);
        }

        int randNum = Random.Range(0, eliteMonsterPrefabs.Length);
        GameObject eliteMonster = Instantiate(eliteMonsterPrefabs[randNum], spawnPoints[spawnPoints.Length - 1].position, Quaternion.identity);
        eliteMonster.transform.SetParent(transform.parent);
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
