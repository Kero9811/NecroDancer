using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }

    public CameraShake shakeCamera;
    public Player player;
    public TimingManager timingManager;
    public ObjectPoolManager pool;
    public bool isMove;
    public bool monsterIsMove;

    public NoteManager noteManager;
    public TilemapRenderer tilemapRenderer;
    public ControlHpUI controlHpUI;
    public WeaponUI weaponUI;
    public ShovelUI shovelUI;
    public CoinText coinText;
    public AudioSource beatAudio;

    // 스폰 시 리스트에 추가
    public List<Skeleton> skeletons = new List<Skeleton>();
    public List<Bat> bats = new List<Bat>();
    public List<Dragon> dragons = new List<Dragon>();
    public List<GreenSlime> greenSlimes = new List<GreenSlime>();
    public List<BlueSlime> blueSlimes = new List<BlueSlime>();


    public List<Vector2> monstersNextPos = new List<Vector2>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        shakeCamera = FindObjectOfType<CameraShake>();
        tilemapRenderer = GameObject.Find("Grid").transform.GetChild(0).GetComponent<TilemapRenderer>();
        beatAudio = GameObject.FindWithTag("BeatManager").GetComponent<AudioSource>();
    }

    public void UpdateOnBPM()
    {
        if (!beatAudio.isPlaying && !player.isDone)
        {
            player.isDead = true;
            player.resultUI.ControlPanel(player.isDead);
        }

        if (player.isDead || player.isDone)
        {
            return;
        }

        noteManager.NoteSpawn();

        if (!isMove)
        {
            foreach (var skeleton in skeletons)
            {
                skeleton.Move();
            }

            foreach (var bat in bats)
            {
                bat.Move();
            }

            foreach (var dragon in dragons)
            {
                dragon.Move();
            }

            foreach (var greenSlime in greenSlimes)
            {
                greenSlime.Move();
            }

            foreach (var blueSlime in blueSlimes)
            {
                blueSlime.Move();
            }

            player.isMiss = true;
            monstersNextPos.Clear();
        }

        tilemapRenderer.enabled = !tilemapRenderer.enabled;
        isMove = false;
    }

    public void PlayerMove()
    {
        foreach (var skeleton in skeletons)
        {
            skeleton.Move();
        }

        foreach (var bat in bats)
        {
            bat.Move();
        }

        foreach (var dragon in dragons)
        {
            dragon.Move();
        }

        foreach (var greenSlime in greenSlimes)
        {
            greenSlime.Move();
        }

        foreach (var blueSlime in blueSlimes)
        {
            blueSlime.Move();
        }

        monstersNextPos.Clear();
        isMove = true;
    }
}