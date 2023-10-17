using System;
using System.Collections.Generic;
using UnityEngine;

public class Solve
{
    private bool[][] gridState;
    private Func<Solve, bool>[] rules;
    private LevelManager levelManager;

    public Solve(GridData gridData, Func<Solve, bool>[] rules)
    {
        gridState = new bool[gridData.rows][];
        for (int i = 0; i < gridData.rows; i++)
            gridState[i] = new bool[gridData.columns];
        this.rules = rules;
    }

    public void MakeMove(int row, int column, bool state)
    {
        gridState[row][column] = state;
        bool[] results = CheckRules();
        if (CheckCompleted(results))
        {
            levelManager.LevelComplete();
            return;
        }

        levelManager.UpdateRules(results);
    }

    public bool[] CheckRules()
    {
        bool[] result = new bool[rules.Length];
        for (int i = 0; i < rules.Length; i++)
        {
            result[i] = rules[i](this);
        }
        return result;
    }
    public bool CheckCompleted(bool[] results)
    {
        foreach (bool result in results)
            if (!result)
                return false;
        return true;
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