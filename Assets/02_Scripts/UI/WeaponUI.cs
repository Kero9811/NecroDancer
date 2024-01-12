using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    Image image;
    public List<Sprite> weaponSprites;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void ChangeWeapon(WeaponType type)
    {
        image.sprite = weaponSprites[(int)type];
    }
}
