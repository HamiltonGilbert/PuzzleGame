using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using static Rules;

public class Grid : MonoBehaviour
{
    private GameObject tilePrefab;
    private int rows;
    private int columns;
    private Solve solve;
    private Func<Solve, bool>[] rules;
    private Tile[][] tiles;

    public bool[] CheckMove()
    {
        bool[] check = new bool[rules.Length];
        for (int i=0; i<rules.Length; i++)
        {
            check[i] = rules[i](solve);
        }
        return check;
    }

    public void CreateGrid(GridData gridData)
    {
        // initialize
        rows = gridData.rows;
        columns = gridData.columns;
        tilePrefab = gridData.tilePrefab;
        solve = new Solve(gridData);
        rules = new Func<Solve, bool>[gridData.ruleNames.Length];
        for (int i = 0; i < rules.Length; i++)
        {
            rules[i] = (Func<Solve, bool>) Delegate.CreateDelegate(typeof(Func<Solve, bool>), typeof(Rules).GetMethod(gridData.ruleNames[i].ToString()));
        }

        // Create physical grid
        int tileDimensions = TileDimensions(rows, columns);
        float tileScale = tileDimensions / 100f;
        Debug.Log("tile dimensions: " + tileDimensions);
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
                rowTiles[c].CreateTile(r, c, this);
            }
            tiles[r] = rowTiles;
        }

        // set fixed tiles
        foreach (GridData.Fixed fixedTile in gridData.fixedTiles)
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
}