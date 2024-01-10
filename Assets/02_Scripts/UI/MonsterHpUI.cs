using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpUI : MonoBehaviour
{
    Monster dragon;
    [SerializeField] Image[] images;
    public Sprite[] sprites = new Sprite[2];
    int emptyHeart;
    int heartCount;


    private void Awake()
    {
        dragon = GetComponentInParent<Dragon>();
        images = GetComponentsInChildren<Image>();
    }
    private void Start()
    {
        heartCount = dragon.currentHp;

        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
        }
    }

    public void ChangeHpUI()
    {
        if (!images[0].gameObject.activeSelf)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].gameObject.SetActive(true);
            }
        }

        int loseHp = dragon.maxHp - dragon.currentHp;
        emptyHeart = loseHp;

        for (int i = 0; i < emptyHeart; i++)
        {
            images[i].sprite = sprites[1];
        }

        for (int i = emptyHeart; i < heartCount; i++)
        {
            images[i].sprite = sprites[0];
        }

    }
}