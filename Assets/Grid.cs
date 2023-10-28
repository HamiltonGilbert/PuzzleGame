using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

/*
 * creates the physical grid
 * determines physical tile placement and size on screen
 * creates Solve and GridData for the grid
*/

public class Grid : MonoBehaviour
{
    private GameObject tilePrefab;
    private int rows;
    private int columns;
    private Tile[][] tiles = new Tile[0][];

    public void CreateGrid(LevelData levelData, LevelManager levelManager)
    {
        DestroyGrid();
        // initialize
        rows = levelData.rows;
        columns = levelData.columns;
        tilePrefab = levelData.tilePrefab;

        // create Solve
        Solve solve = new(levelData, levelManager);

        // create physical grid
        int tileDimensions = TileDimensions(rows, columns);
        float tileScale = tileDimensions / 100f;
        int xPos = (columns * -1 * tileDimensions / 2) + tileDimensions / 2;
        int yPos = (rows * tileDimensions / 2) - tileDimensions / 2;
        tiles = new Tile[rows][];
        for (int r = 0; r < rows; r++)
        {
            Tile[] rowTiles = new Tile[columns];
            for (int c = 0; c < columns; c++)
            {
                GameObject tempObject = Instantiate(tilePrefab, gameObject.transform);
                tempObject.transform.position = new Vector3(transform.position.x + xPos + (tileDimensions * c), transform.position.y + yPos - (tileDimensions * r), 0);
                tempObject.transform.localScale = new Vector3(tileScale, tileScale, 1f);
                rowTiles[c] = tempObject.GetComponent<Tile>();
                rowTiles[c].CreateTile(r, c, solve);
            }
            tiles[r] = rowTiles;
        }

        // ----- FILL IN Grid -----
        // set fixed tiles
        foreach (LevelData.Fixed fixedTile in levelData.fixedTiles)
        {
            tiles[fixedTile.row - 1][fixedTile.column - 1].SetFixed(fixedTile.state);
        }
        // set numbered tiles
        LevelData.Numbered[] numberedTiles = levelData.numberedTiles;
        int[][] numberedTilesIndices = new int[numberedTiles.Length][];
        for (int i = 0; i < numberedTilesIndices.Length; i++)
        {
            tiles[numberedTiles[i].row - 1][numberedTiles[i].column - 1].SetNumber(numberedTiles[i].number);
        }
        // remove tiles
        foreach (LevelData.Remove range in levelData.removeRanges)
            for (int i=range.start-1; i < range.finish; i++)
            {
                if (range.row)
                {
                    Destroy(tiles[range.number - 1][i].gameObject);
                }
                else
                {
                    Destroy(tiles[i][range.number - 1].gameObject);
                }
            }
    }

    public void ResetGrid()
    {
        foreach (Tile[] row in tiles)
            foreach (Tile tile in row)
                tile.ResetState();
    }

    public int TileDimensions(int rows, int columns)
    {
        // Replace with canvas-based sizing
        int width = 920 / columns;
        int height = 560 / rows;
        if (width < height)
            return width;
        return height;
    }

    public void DestroyGrid()
    {
        for (int i = 0; i < tiles.Length; i++)
            for (int j = 0; j < tiles[i].Length; j++)
                if (tiles[i][j] != null)
                    Destroy(tiles[i][j].gameObject);
    }
}