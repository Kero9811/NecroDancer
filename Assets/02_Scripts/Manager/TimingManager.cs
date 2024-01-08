//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TimingManager : MonoBehaviour
//{
//    public List<GameObject> boxNoteList = new List<GameObject>();

//    [SerializeField] Transform Center = null;
//    [SerializeField] RectTransform[] timingRect = null;
//    Vector2[] timingBoxs = null;

//    private void Start()
//    {
//        timingBoxs = new Vector2[timingRect.Length];

//        for (int i = 0; i < timingRect.Length; i++)
//        {
//            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
//                              Center.localPosition.x + timingRect[i].rect.width / 2);
//        }
//    }

//    public void CheckTiming()
//    {
//        for (int i = 0; i < boxNoteList.Count; i++)
//        {
//            float t_notePosX = boxNoteList[i].transform.localPosition.x;

//            for (int x = 0; x < timingBoxs.Length; x++)
//            {
//                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
//                {
//                    //print("Hit" + x);
//                    GameManager.Instance.player.isCorrect = true;
//                    boxNoteList.Remove(boxNoteList[i]);
//                    return;
//                }
//            }
//        }
//        GameManager.Instance.player.isCorrect = false;
//        //print("Out");
//    }
//}


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
