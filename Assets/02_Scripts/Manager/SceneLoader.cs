using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private SceneLoader instance;
    public SceneLoader Instance { get { return instance; } }

    //public int currentStageIdx;

    //private string[] stages = new string[] { "StartScene", "Stage_1", "Stage_2", "Stage_3" };
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //currentStageIdx = GameManager.Instance.currentSceneIdx;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1.0f;
    }

    public void LoadGameScene(string sceneName)
    {
        GameManager.Instance.skeletons.Clear();
        GameManager.Instance.bats.Clear();
        GameManager.Instance.dragons.Clear();
        GameManager.Instance.blueSlimes.Clear();
        GameManager.Instance.greenSlimes.Clear();

        SceneManager.LoadScene(sceneName);
    }
}
