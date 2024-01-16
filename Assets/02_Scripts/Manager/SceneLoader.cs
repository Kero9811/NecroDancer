using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private SceneLoader instance;
    public SceneLoader Instance { get { return instance; } }

    public int nextStageIdx;

    private string[] stages = new string[] { "StartScene", "Stage_1", "Stage_2", "Stage_3" };
    
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
        nextStageIdx = 1;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(stages[nextStageIdx]);
    }
}
