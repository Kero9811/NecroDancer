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
        // ��Ʈ�� ������ ���� �ȿ� �ִٸ� Ÿ�̹��� �¾Ҵٰ� ��� �� ��Ʈ�� ����Ʈ���� ����
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
            // �׷��� �ʴٸ� Ÿ�̹��� ������ ���ߴٰ� ���
            GameManager.Instance.player.isCorrect = false;
        }

    }
}
