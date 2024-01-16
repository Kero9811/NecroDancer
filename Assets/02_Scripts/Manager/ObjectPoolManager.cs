using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int idx = 0; idx < pools.Length; idx++)
        {
            pools[idx] = new List<GameObject>();
        }
    }

    public GameObject Get(int idx)
    {
        GameObject select = null;

        foreach (GameObject item in pools[idx])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefabs[idx], transform);
            pools[idx].Add(select);
        }

        return select;
    }
}
