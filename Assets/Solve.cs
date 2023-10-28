using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * creates GridData
*/

public class Solve
{
    private readonly GridData gridData;
    private Func<GridData, bool>[] rules;
    public int NumberOfRules { get => rules.Length; }
    private readonly LevelManager levelManager;

    public Solve(LevelData levelData, LevelManager levelManager)
    {
        this.levelManager = levelManager;
        gridData = Initialize(levelData);
        rules = GetRules(levelData);
        levelManager.UpdateRules(CheckRules());
    }
    // called from tile
    public void MakeMove()
    {
        bool[] results = CheckRules();
        levelManager.UpdateRules(results);
        levelManager.SetLevelCompleted(CheckCompleted(results));
        //PrintGridState();
    }
    public void SetState(int row, int column, bool state)
    {
        gridData.UpdateTileState(row, column, state);
    }

    public bool[] CheckRules()
    {
        bool[] result = new bool[rules.Length];
        for (int i = 0; i < rules.Length; i++)
        {
            result[i] = rules[i](gridData);
        }
        return result;
    }
    
    public bool CheckCompleted(bool[] results)
    {
        foreach (bool result in results)
            if (!result)
                return false;
        return true;
        //foreach (bool?[] column in gridData.gridState)
        //    foreach (bool? state in column)
        //        if (state == null)
        //            return false;
    }

    private Func<GridData, bool>[] GetRules(LevelData levelData)
    {
        rules = new Func<GridData, bool>[levelData.GetRules().Length];
        for (int i = 0; i < rules.Length; i++)
        {
            Debug.Log(levelData.GetRules()[i]);
            rules[i] = (Func<GridData, bool>)Delegate.CreateDelegate(typeof(Func<GridData, bool>), typeof(Rules).GetMethod(levelData.GetRules()[i].ToString()));
        }
        return rules;
    }

    public void PrintGridState()
    {
        string result = "";
        foreach (bool?[] row in gridData.gridState)
        {
            string temp = "";
            foreach (bool? tileState in row)
            {
                if (tileState == null)
                    temp += "|| null ";
                else if ((bool)tileState)
                    temp += "|| T ";
                else
                    temp += "|| F ";
            }
            result += temp + "||\n";
        }
        Debug.Log(result);
    }

    public GridData Initialize(LevelData levelData)
    {
        // create initial gridState
        bool?[][] tempGridState = new bool?[levelData.rows][];
        for (int r = 0; r < levelData.rows; r++)
        {
            tempGridState[r] = new bool?[levelData.columns];
            for (int c = 0; c < levelData.columns; c++)
                tempGridState[r][c] = false;
        }
        // set removed tiles to null
        foreach (LevelData.Remove range in levelData.removeRanges)
            for (int i = range.start - 1; i < range.finish; i++)
                if (range.row)
                    tempGridState[range.number - 1][i] = null;
                else
                    tempGridState[i][range.number - 1] = null;
        // set numbered tiles
        LevelData.Numbered[] numberedTiles = levelData.numberedTiles;
        int[][] numberedTilesIndices = new int[numberedTiles.Length][];
        for (int i = 0; i < numberedTilesIndices.Length; i++)
            numberedTilesIndices[i] = new int[] { numberedTiles[i].row, numberedTiles[i].column, numberedTiles[i].number };
        // update fixed tiles
        foreach (LevelData.Fixed fixedTile in levelData.fixedTiles)
                tempGridState[fixedTile.row - 1][fixedTile.column - 1] = fixedTile.state;

        return new(tempGridState, numberedTilesIndices);
    }
}

//public void NewGrid(bool[][] newGrid, Action<int[][]>[] newRules)
//{
//    grid = newGrid;
//    ResetGrid();
//}
//public void ResetGrid()
//{
//    grid = defaultGrid;
//}
//public void SetTile(int[] index, int value)
//{
//    grid[index[0]][index[1]] = value;
//}