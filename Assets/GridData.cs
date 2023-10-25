using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    public bool?[][] gridState;
    public int[][] numberedTiles; // [row, column, number]

    public GridData(bool?[][] gridState, LevelData.Numbered[] numberedTiles)
    {
        this.gridState = gridState;
        this.numberedTiles = new int[numberedTiles.Length][];
        for (int i = 0; i < numberedTiles.Length; i++)
            this.numberedTiles[i] = new int[] { numberedTiles[i].row, numberedTiles[i].column, numberedTiles[i].number };
    }

    public void UpdateGridState(int row, int column, bool? state)
    {
        gridState[row][column] = state;
    }
}
