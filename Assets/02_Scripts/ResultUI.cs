using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResultUI : MonoBehaviour
{
    EventSystem eventSystem;

    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    public void ControlPanel()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        Time.timeScale = 0f;

        //GameManager.Instance.beatAudio.Pause();

        eventSystem.SetSelectedGameObject(null);
    }
}
