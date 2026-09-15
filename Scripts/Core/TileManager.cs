using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private int gridWidth = 6;
    [SerializeField] private int gridHeight = 8;
    [SerializeField] private float tileSize = 1f;
    [SerializeField] private float spacing = 0.1f;

    private Tile[,] tileGrid;
    private List<Tile> selectedTiles = new List<Tile>();
    private bool isAnimating = false;

    private void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        tileGrid = new Tile[gridWidth, gridHeight];
        string[] tileTypes = { "Red", "Blue", "Green", "Yellow", "Purple", "Orange" };

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GameObject tileObj = Instantiate(tilePrefab, GetTilePosition(x, y), Quaternion.identity);
                Tile tile = tileObj.GetComponent<Tile>();
                
                string tileType = tileTypes[Random.Range(0, tileTypes.Length)];
                tile.Initialize(x, y, tileType, OnTileSelected);
                
                tileGrid[x, y] = tile;
            }
        }
    }

    private Vector3 GetTilePosition(int x, int y)
    {
        return new Vector3(x * (tileSize + spacing), -y * (tileSize + spacing), 0);
    }

    private void OnTileSelected(Tile tile)
    {
        if (isAnimating) return;

        if (selectedTiles.Contains(tile))
        {
            selectedTiles.Remove(tile);
            tile.Deselect();
        }
        else
        {
            if (selectedTiles.Count > 0 && !AreAdjecent(selectedTiles[selectedTiles.Count - 1], tile))
            {
                return;
            }

            if (selectedTiles.Count > 0 && selectedTiles[selectedTiles.Count - 1].GetTileType() != tile.GetTileType())
            {
                return;
            }

            selectedTiles.Add(tile);
            tile.Select();
        }

        if (selectedTiles.Count >= 3)
        {
            StartCoroutine(RemoveSelectedTiles());
        }
    }

    private bool AreAdjecent(Tile tile1, Tile tile2)
    {
        int dx = Mathf.Abs(tile1.GetGridX() - tile2.GetGridX());
        int dy = Mathf.Abs(tile1.GetGridY() - tile2.GetGridY());
        return (dx == 1 && dy == 0) || (dx == 0 && dy == 1);
    }

    private System.Collections.IEnumerator RemoveSelectedTiles()
    {
        isAnimating = true;

        int comboCount = selectedTiles.Count;
        long scoreGained = comboCount * 10;

        foreach (var tile in selectedTiles)
        {
            tile.Remove();
        }

        yield return new WaitForSeconds(0.3f);

        ApplyGravity();
        FillEmptySpaces();
        selectedTiles.Clear();
        isAnimating = false;
    }

    private void ApplyGravity()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = gridHeight - 1; y >= 0; y--)
            {
                if (tileGrid[x, y] == null)
                {
                    for (int yAbove = y - 1; yAbove >= 0; yAbove--)
                    {
                        if (tileGrid[x, yAbove] != null)
                        {
                            tileGrid[x, y] = tileGrid[x, yAbove];
                            tileGrid[x, y].SetGridPosition(x, y);
                            tileGrid[x, yAbove] = null;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void FillEmptySpaces()
    {
        string[] tileTypes = { "Red", "Blue", "Green", "Yellow", "Purple", "Orange" };

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (tileGrid[x, y] == null)
                {
                    GameObject tileObj = Instantiate(tilePrefab, GetTilePosition(x, y), Quaternion.identity);
                    Tile tile = tileObj.GetComponent<Tile>();
                    string tileType = tileTypes[Random.Range(0, tileTypes.Length)];
                    tile.Initialize(x, y, tileType, OnTileSelected);
                    tileGrid[x, y] = tile;
                }
            }
        }
    }
}
