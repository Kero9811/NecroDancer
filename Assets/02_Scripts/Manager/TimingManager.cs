using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform timingRect = null;

    public void CheckTiming()
    {
        // 노트가 정해진 영역 안에 있다면 타이밍이 맞았다고 기록 후 노트를 리스트에서 제거
        if (boxNoteList[0].transform.localPosition.x > Center.localPosition.x - timingRect.rect.width / 2 &&
            boxNoteList[1].transform.localPosition.x < Center.localPosition.x + timingRect.rect.width / 2)
        {
            GameManager.Instance.player.isCorrect = true;
            boxNoteList.Remove(boxNoteList[0]);
            boxNoteList.Remove(boxNoteList[0]);
            return;
        }
        else
        {
            // 그렇지 않다면 타이밍을 맞추지 못했다고 기록
            GameManager.Instance.player.isCorrect = false;
        }

    }
}
