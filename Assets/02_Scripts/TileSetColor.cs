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
        FirstTileColorChange();
    }

    public void FirstTileColorChange()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (var tilePos in bounds.allPositionsWithin)
        {
            tilemap.SetTileFlags(tilePos, TileFlags.None);
            tilemap.SetColor(tilePos, Color.black);
        }
    }

    public void TileColorChange()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (var tilePos in bounds.allPositionsWithin)
        {
            if (GetTileColor(tilePos) == Color.white)
            {
                tilemap.SetColor(tilePos, personalColor);
            }
            else
            {
                tilemap.SetColor(tilePos, Color.black);
            }
        }
    }

    private Color GetTileColor(Vector3Int tilePos)
    {
        TileBase tile = tilemap.GetTile(tilePos);
        if (tile == null)
        {
            return Color.black;
        }

        Tile tileData = (Tile)tile;

        Color tileColor = tileData.color;

        return tileColor;
    }
}
