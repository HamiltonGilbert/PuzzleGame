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
    private GameObject[][] tileObjects;

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
            rules[i] = (Func<Solve, bool>)Delegate.CreateDelegate(typeof(Rules), typeof(Rules).GetMethod(gridData.ruleNames[i].ToString()));
        }

        // Create physical grid
        int tileDimensions = TileDimensions(rows, columns);
        float tileScale = tileDimensions / 100f;
        Debug.Log("tile dimensions: " + tileDimensions);
        int xPos = (columns * -1 * tileDimensions / 2) + tileDimensions / 2;
        int yPos = (rows * -1 * tileDimensions / 2) + tileDimensions / 2;
        tileObjects = new GameObject[rows][];
        for (int r = 0; r < rows; r++)
        {
            GameObject[] rowObjects = new GameObject[columns];
            for (int c = 0; c < columns; c++)
            {
                GameObject tempTile = Instantiate(tilePrefab, gameObject.transform);
                tempTile.transform.position = new Vector3(transform.position.x + xPos + (tileDimensions * c),transform.position.y + yPos + (tileDimensions * r), 0);
                tempTile.transform.localScale = new Vector3(tileScale, tileScale, 1f);
                tempTile.GetComponent<Tile>().CreateTile(r, c, this);
                rowObjects[c] = tempTile;
            }
            tileObjects[r] = rowObjects;
        }
    }

    public void ResetGrid()
    {
        foreach (GameObject[] row in tileObjects)
        {
            foreach (GameObject tile in row)
            {
                tile.GetComponent<Tile>().ResetState();
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