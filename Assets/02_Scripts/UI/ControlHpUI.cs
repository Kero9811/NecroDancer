using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlHpUI : MonoBehaviour
{
    Player player;
    [SerializeField] Image[] images;
    public Sprite[] sprites = new Sprite[3];
    bool isHalfHeart;
    int halfHeartIndex;
    int emptyHeart;
    int fullHeart;
    int heartCount;


    private void Awake()
    {
        player = FindObjectOfType<Player>();
        images = GetComponentsInChildren<Image>();
    }

    private void Start()
    {
        images[3].gameObject.SetActive(false);
        images[4].gameObject.SetActive(false);
        heartCount = player.currentHp / 2;
    }

    public void ChangeHpUI()
    {
        int loseHp = player.maxHp - player.currentHp;
        emptyHeart = loseHp / 2;
        fullHeart = heartCount - emptyHeart;
        if (loseHp % 2 == 1)
        {
            isHalfHeart = true;
            fullHeart--;
        }
        halfHeartIndex = emptyHeart;

        if (isHalfHeart)
        {
            if (emptyHeart != 0)
            {
                for (int i = 0; i < emptyHeart; i++)
                {
                    images[i].sprite = sprites[2];
                }

                images[halfHeartIndex].sprite = sprites[1];

                for (int i = halfHeartIndex + 1; i < heartCount; i++)
                {
                    images[i].sprite = sprites[0];
                }
            }
            else if (emptyHeart == 0)
            {
                images[halfHeartIndex].sprite = sprites[1];

                for (int i = halfHeartIndex + 1; i < heartCount; i++)
                {
                    images[i].sprite = sprites[0];
                }
            }
        }
        else
        {
            for (int i = 0; i < emptyHeart; i++)
            {
                images[i].sprite = sprites[2];
            }

            for (int i = halfHeartIndex; i < heartCount; i++)
            {
                images[i].sprite = sprites[0];
            }
        }

        isHalfHeart = false;
        CheckMaxHp();
    }

    public void CheckMaxHp()
    {
        if (player.maxHp == 8)
        {
            images[3].gameObject.SetActive(true);
        }
        else if(player.maxHp == 10)
        {
            images[4].gameObject.SetActive(true);
        }
        else
        {
            return;
        }
    }
}
