using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] AudioSource mainCamAudio;

    private void Awake()
    {
        mainCamAudio = Camera.main.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            mainCamAudio.volume = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            mainCamAudio.volume = .1f;
        }
    }
}
