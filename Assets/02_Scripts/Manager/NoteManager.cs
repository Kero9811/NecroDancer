using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int noteBpm = 0;

    [SerializeField]
    Transform leftNoteSpawnTf;
    [SerializeField]
    Transform rightNoteSpawnTf;
    [SerializeField]
    GameObject notePrefab;
    [SerializeField]
    Transform heart;
    Vector3 originScale;

    TimingManager timingManager;

    private void Start()
    {
        originScale = notePrefab.transform.localScale;
        timingManager = GetComponent<TimingManager>();
        noteBpm = GameManager.Instance.bpm;
    }

    public void NoteSpawn()
    {
        GameObject leftNote = GameManager.Instance.pool.Get(0);
        GameObject rightNote = GameManager.Instance.pool.Get(0);

        leftNote.transform.position = leftNoteSpawnTf.position;
        rightNote.transform.position = rightNoteSpawnTf.position;

        leftNote.transform.rotation = Quaternion.identity;
        rightNote.transform.rotation = Quaternion.identity;

        rightNote.GetComponent<Note>().isRight = true;

        leftNote.transform.SetParent(transform);
        rightNote.transform.SetParent(transform);

        timingManager.boxNoteList.Add(leftNote);
        timingManager.boxNoteList.Add(rightNote);

        leftNote.transform.localScale = originScale;
        rightNote.transform.localScale = originScale;

        heart.SetAsLastSibling();
    }
}
