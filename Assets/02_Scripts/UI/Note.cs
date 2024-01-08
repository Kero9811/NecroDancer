using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400;
    public bool isRight = false;

    private void Update()
    {
        if (!isRight)
        {
            transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
        }
        else
        {
            transform.localPosition += Vector3.left* noteSpeed * Time.deltaTime;
        }
    }
}
