using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlime : Monster
{
    new void Start()
    {
        base.Start();

        GameManager.Instance.greenSlimes.Add(gameObject.GetComponent<GreenSlime>());
    }

    public void Move()
    {
        GameManager.Instance.monstersNextPos.Add(transform.position);
    }
}
