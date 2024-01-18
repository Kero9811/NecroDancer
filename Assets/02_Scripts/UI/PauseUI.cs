using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    EventSystem eventSystem;

    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ControlPanel();
        }
    }

    public void ControlPanel()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }

        Time.timeScale = (Time.timeScale > 0) ? 0 : 1f;

        if (GameManager.Instance.beatAudio.isPlaying)
        {
            GameManager.Instance.beatAudio.Pause();
        }
        else
        {
            GameManager.Instance.beatAudio.Play();
        }
        eventSystem.SetSelectedGameObject(null);
    }
}
