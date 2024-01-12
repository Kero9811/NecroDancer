using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRGB : MonoBehaviour
{
    public Tilemap[] tilemaps;
    public TileBase targetTile;

    public Color[] originColors;

    private void Awake()
    {
        originColors = new Color[tilemaps.Length];
    }

    private void Start()
    {
        for (int i = 0; i < tilemaps.Length; i++)
        {
            originColors[i] = tilemaps[i].color;
        }
    }

    void Update()
    {
        Vector3Int playerPos = tilemaps[0].WorldToCell(transform.position);

        for (int i = 0; i < tilemaps.Length; i++)
        {
            for (int x = playerPos.x - 2; x <= playerPos.x + 2; x++)
            {
                for (int y = playerPos.y - 2; y <= playerPos.y + 2; y++)
                {
                    Vector3Int tilePos = new Vector3Int(x, y, 0);
                    tilemaps[i].SetColor(tilePos, originColors[i]);
                    tilemaps[i].SetTile(tilePos, targetTile);
                }
            }
        }
    }
}
