using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private GameObject tilePrefab;
    private int rows;
    private int columns;
    private Tile[][] tiles;

    public void CreateGrid(LevelData levelData, Solve solve)
    {
        // initialize
        rows = levelData.rows;
        columns = levelData.columns;
        tilePrefab = levelData.tilePrefab;

        // Create physical grid
        int tileDimensions = TileDimensions(rows, columns);
        float tileScale = tileDimensions / 100f;
        int xPos = (columns * -1 * tileDimensions / 2) + tileDimensions / 2;
        int yPos = (rows * -1 * tileDimensions / 2) + tileDimensions / 2;
        tiles = new Tile[rows][];
        for (int r = 0; r < rows; r++)
        {
            Tile[] rowTiles = new Tile[columns];
            for (int c = 0; c < columns; c++)
            {
                GameObject tempObject = Instantiate(tilePrefab, gameObject.transform);
                tempObject.transform.position = new Vector3(transform.position.x + xPos + (tileDimensions * c),transform.position.y + yPos + (tileDimensions * r), 0);
                tempObject.transform.localScale = new Vector3(tileScale, tileScale, 1f);
                rowTiles[c] = tempObject.GetComponent<Tile>();
                rowTiles[c].CreateTile(r, c, solve);
            }
            tiles[r] = rowTiles;
        }

        // set fixed tiles
        foreach (LevelData.Fixed fixedTile in levelData.fixedTiles)
            tiles[fixedTile.row-1][fixedTile.column-1].SetFixed(fixedTile.value);
    }

    public void ResetGrid()
    {
        foreach (Tile[] row in tiles)
        {
            foreach (Tile tile in row)
            {
                tile.ResetState();
            }
        }
    }

    public int TileDimensions(int rows, int columns)
    {
        // Replace with canvas-based sizing
        int width = 920 / columns;
        int height = 632 / rows;
        if (width < height)
            return width;
        return height;
    }

    public void EndLevel()
    {
        foreach (Tile[] column in tiles)
            foreach (Tile tile in column)
                tile.enabled = false;
    }
}