using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;
    public static SceneLoader Instance { get { return instance; } }

    public string[] stages = new string[] { "StartScene", "Stage_1", "Stage_2", "Stage_3" };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1.0f;

        if (PlayerInfo.Instance != null)
        {
            PlayerInfo.Instance.currentStageIdx = 1;
            PlayerInfo.Instance.playerMaxHp = 6;
            PlayerInfo.Instance.playerCurrentHp = 6;
            PlayerInfo.Instance.playerDamage = 1;
            PlayerInfo.Instance.playerDigPower = 1;
            PlayerInfo.Instance.playerWeapon = WeaponType.Dagger;
            PlayerInfo.Instance.playerShovel = ShovelGrade.Iron;
            PlayerInfo.Instance.playerCoinAmount = 0;
            PlayerInfo.Instance.playerHorizontalRange = 1;
            PlayerInfo.Instance.playerVerticalRange = 1;
        }
        
        SceneManager.LoadScene(sceneName);
    }

    public void LoadGameScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.player.isDead = false;
        SceneManager.LoadScene(stages[PlayerInfo.Instance.currentStageIdx]);
    }
}
