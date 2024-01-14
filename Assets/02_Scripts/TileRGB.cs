using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRGB : MonoBehaviour
{
    public Tilemap[] tilemaps;
    public int sightRange;

    private void Start()
    {
        ChangeTileColor();
    }

    public void ChangeTileColor()
    {
        Vector3Int playerPos = tilemaps[0].WorldToCell(transform.position);

        for (int i = 0; i < tilemaps.Length; i++)
        {
            for (int x = playerPos.x - sightRange; x <= playerPos.x + sightRange; x++)
            {
                for (int y = playerPos.y - sightRange; y <= playerPos.y + sightRange; y++)
                {
                    Vector3Int tilePos = new Vector3Int(x, y, 0);
                    tilemaps[i].SetTileFlags(tilePos, TileFlags.None);
                    tilemaps[i].SetColor(tilePos, Color.white);
                }
            }
        }
    }
}