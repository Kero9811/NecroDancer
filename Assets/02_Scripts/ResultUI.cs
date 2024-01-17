using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResultUI : MonoBehaviour
{
    EventSystem eventSystem;
    public TextMeshProUGUI text;

    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    public void ControlPanel(bool isDead)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        Time.timeScale = 0f;

        text.text = isDead ? "You Died" : "Clear!";

        eventSystem.SetSelectedGameObject(null);
    }
}
