using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public LayerMask targetLayer;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1, targetLayer);
        if (hit.collider != null)
        {
            print(hit.collider.name);
        }
    }
}
