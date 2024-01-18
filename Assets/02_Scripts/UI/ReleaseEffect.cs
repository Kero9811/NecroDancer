using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseEffect : MonoBehaviour
{
    public void DestroyEffect()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
