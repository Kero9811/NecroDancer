using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }

    public CameraShake shakeCamera;
    public Player player;
    public TimingManager timingManager;
    public ObjectPoolManager pool;
    public int bpm = 0;
    private double currentTime = 0d;
    public bool isMove;
    public bool monsterIsMove;

    public NoteManager noteManager;
    public TilemapRenderer tilemapRenderer;
    
    // 스폰 시 리스트에 추가
    public List<AStarMoving> aStarMovings = new List<AStarMoving>();
    public List<SlideMoving> slideMovings = new List<SlideMoving>();

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
        shakeCamera = Camera.main.GetComponent<CameraShake>();
        tilemapRenderer = GameObject.Find("Grid").transform.GetChild(0).GetComponent<TilemapRenderer>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 60d / bpm)
        {
            // 노트 스폰
            noteManager.NoteSpawn();

            if(!isMove)
            {
                foreach (var aStar in aStarMovings)
                {
                    aStar.Move();
                }

                foreach (var slide in slideMovings)
                {
                    slide.Move();
                }

                tilemapRenderer.enabled = !tilemapRenderer.enabled;
            }

            isMove = false;
            currentTime -= 60d / bpm;
        }

    }

    public void playerMove()
    {
        foreach (var aStar in aStarMovings)
        {
            aStar.Move();
        }
        tilemapRenderer.enabled = !tilemapRenderer.enabled;

        isMove = true;
    }
}
