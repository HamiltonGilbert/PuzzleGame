using System;
using System.Collections.Generic;
using UnityEngine;

public class Solve
{
    private int[][] grid;
    private int[][] defaultGrid;

    public void NewGrid(int[][] newGrid, Action<int[][]>[] newRules)
    {
        defaultGrid = newGrid;
        ResetGrid();
    }
    public void ResetGrid()
    {
        grid = defaultGrid;
    }
    public void SetTile(int[] index, int value)
    {
        grid[index[0]][index[1]] = value;
    }

    public bool checkSolved(Func<Solve, bool>[] rules)
    {
        foreach (Func<Solve, bool> rule in rules)
            if (!rule(this))
                return false;
        return true;
    }
}
