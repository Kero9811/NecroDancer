using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeat : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Note")
        {
            animator.SetTrigger("Hit");
            GameManager.Instance.timingManager.boxNoteList.Remove(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
