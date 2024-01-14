using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSetColor : MonoBehaviour
{
    public Tilemap tilemap;
    public Color personalColor;
    public bool visited;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void Start()
    {
        TileColorChange();
    }

    private void Update()
    {

    }

    private void TileColorChange()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (var tilePos in bounds.allPositionsWithin)
        {
            tilemap.SetTileFlags(tilePos, TileFlags.None);
            tilemap.SetColor(tilePos, Color.black);
        }
    }
}
