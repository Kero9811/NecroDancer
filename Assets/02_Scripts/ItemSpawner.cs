using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] itemPrefabs;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildrenExceptMe(spawnPoints);
    }

    private void Start()
    {
        Invoke("Spawn", .5f);
    }

    void Spawn()
    {
        int[] checkList = new int[spawnPoints.Length];

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int randomNum = Random.Range(1, itemPrefabs.Length + 1);

            if (checkList.Contains(randomNum))
            {
                i--;
                continue;
            }

            checkList[i] = randomNum;
            GameObject item = Instantiate(itemPrefabs[randomNum - 1], spawnPoints[i].position, Quaternion.identity);
            item.transform.SetParent(transform.parent);
        }
    }

    private Transform[] GetComponentsInChildrenExceptMe(Transform[] transforms)
    {
        transforms = GetComponentsInChildren<Transform>();
        List<Transform> list = new List<Transform>(transforms);
        list.RemoveAt(0);
        transforms = list.ToArray();

        return transforms;
    }
}
