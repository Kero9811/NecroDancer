using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShovelUI : MonoBehaviour
{
    Image image;
    public List<Sprite> shovelSprites;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void ChangeShovel(ShovelGrade grade)
    {
        image.sprite = shovelSprites[(int)grade];
    }
}
