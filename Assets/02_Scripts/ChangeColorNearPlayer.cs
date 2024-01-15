using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeColorNearPlayer : MonoBehaviour
{
    public Tilemap[] tilemaps;
    public SpriteRenderer[] sprites;
    public int sightRange;
    public Color alreadyCheckView;

    List<Vector3Int> beforeChangedCellList = new List<Vector3Int>();
    List<SpriteRenderer> beforeChangedObjectList = new List<SpriteRenderer>();

    private void Start()
    {
        for (int i = 0; i < tilemaps.Length; i++)
        {
            for (int x = 0; x <= sightRange; x++)
            {
                for (int y = 0; y <= sightRange; y++)
                {
                    Vector3Int tilePos = new Vector3Int(x, y, 0);
                    beforeChangedCellList.Add(tilePos);
                }
            }
        }

        ChangeTileColor();
    }

    public void ChangeTileColor()
    {
        List<Vector3Int> currentChangedCellList = new List<Vector3Int>();

        Vector3Int playerPos = tilemaps[0].WorldToCell(transform.position);

        for (int i = 0; i < tilemaps.Length; i++)
        {
            for (int x = playerPos.x - sightRange; x <= playerPos.x + sightRange; x++)
            {
                for (int y = playerPos.y - sightRange; y <= playerPos.y + sightRange; y++)
                {
                    Vector3Int tilePos = new Vector3Int(x, y, 0);
                    currentChangedCellList.Add(tilePos);
                    tilemaps[i].SetColor(tilePos, Color.white);
                }
            }
        }

        beforeChangedCellList = beforeChangedCellList.Except(currentChangedCellList).ToList();

        for (int i = 0; i < tilemaps.Length; i++)
        {
            foreach (var tilePos in beforeChangedCellList)
            {
                tilemaps[i].SetColor(tilePos, alreadyCheckView);
            }
        }

        beforeChangedCellList = new List<Vector3Int>(currentChangedCellList);
    }

    public void SeeObject(SpriteRenderer renderer)
    {
        renderer.color = Color.white;
    }

    public void DontSeeObject(SpriteRenderer renderer)
    {
        renderer.color = alreadyCheckView;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out SpriteRenderer renderer))
        {
            SeeObject(renderer);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out SpriteRenderer renderer))
        {
            DontSeeObject(renderer);
        }
    }
}