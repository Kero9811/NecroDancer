using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeAmount = 0.2f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    public void StartShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float currentTime = 0f;

        while (currentTime < shakeDuration)
        {
            Vector3 newPos = originalPosition + Random.insideUnitSphere * shakeAmount;

            transform.position = newPos;

            currentTime += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition;
    }
}
