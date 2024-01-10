using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    Image image;
    public List<Sprite> sprites;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void ChangeWeapon(WeaponType type)
    {
        image.sprite = sprites[(int)type];
    }
}
