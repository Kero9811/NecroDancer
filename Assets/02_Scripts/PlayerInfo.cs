using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    private static PlayerInfo instance;
    public static PlayerInfo Instance { get { return instance; } }

    public int playerMaxHp;
    public int playerCurrentHp;
    public int playerDamage;
    public int playerDigPower;
    public WeaponType playerWeapon;
    public ShovelGrade playerShovel;
    public int playerCoinAmount;
    public int playerHorizontalRange;
    public int playerVerticalRange;

    public int currentStageIdx;

    public int clearCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        DownloadInfo();
    }

    public void DownloadInfo()
    {
        currentStageIdx++;
        playerMaxHp = GameManager.Instance.player.maxHp;
        playerCurrentHp = GameManager.Instance.player.currentHp;
        playerDamage = GameManager.Instance.player.damage;
        playerDigPower = GameManager.Instance.player.digDamage;
        playerWeapon = GameManager.Instance.player.weaponType;
        playerShovel = GameManager.Instance.player.shovelGrade;
        playerCoinAmount = GameManager.Instance.player.coinAmount;
        playerHorizontalRange = GameManager.Instance.player.horizontalRange;
        playerVerticalRange = GameManager.Instance.player.verticalRange;
    }

    public void UpdatePlayerInfo()
    {
        GameManager.Instance.player.maxHp = playerMaxHp; 
        GameManager.Instance.player.currentHp = playerCurrentHp;
        GameManager.Instance.player.damage = playerDamage;
        GameManager.Instance.player.digDamage = playerDigPower;
        GameManager.Instance.player.weaponType = playerWeapon;
        GameManager.Instance.player.shovelGrade = playerShovel;
        GameManager.Instance.player.coinAmount = playerCoinAmount;
        GameManager.Instance.player.horizontalRange = playerHorizontalRange;
        GameManager.Instance.player.verticalRange = playerVerticalRange;

        GameManager.Instance.controlHpUI.ChangeHpUI();
        GameManager.Instance.weaponUI.ChangeWeapon(playerWeapon);
        GameManager.Instance.shovelUI.ChangeShovel(playerShovel);
        GameManager.Instance.coinText.ChangeCoinUI();
    }
}
