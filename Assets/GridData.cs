using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    public bool?[][] gridState;
    public int[][] numberedTilesIndices; // [row, column, number]

    public GridData(bool?[][] gridState, int[][] numberedTilesIndices)
    {
        this.gridState = gridState;
        this.numberedTilesIndices = numberedTilesIndices;
    }
    // updated from Solve
    public void UpdateTileState(int row, int column, bool state)
    {
        gridState[row][column] = state;
    }
}
