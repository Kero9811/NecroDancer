using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlime : Monster
{
    public void Move()
    {
        GameManager.Instance.monstersNextPos.Add(transform.position);
        CheckDistance();
    }
}
