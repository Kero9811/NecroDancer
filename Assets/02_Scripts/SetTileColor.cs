using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetTileColor : MonoBehaviour
{
    public Tilemap tilemap;
    public Color personalColor;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        FirstTileColorChange();
    }

    public void FirstTileColorChange()
    {
        foreach (var tilePos in tilemap.cellBounds.allPositionsWithin)
        {
            tilemap.SetTileFlags(tilePos, TileFlags.None);
            tilemap.SetColor(tilePos, Color.black);
        }
    }
}
