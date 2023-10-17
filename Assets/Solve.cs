using System;
using System.Collections.Generic;
using UnityEngine;

public class Solve
{
    private bool[][] grid;
    //private bool[][] defaultGrid;

    public Solve(GridData gridData)
    {
        //gridData
    }

    public void NewGrid(bool[][] newGrid, Action<int[][]>[] newRules)
    {
        grid = newGrid;
        //ResetGrid();
    }
    //public void ResetGrid()
    //{
    //    grid = defaultGrid;
    //}
    //public void SetTile(int[] index, int value)
    //{
    //    grid[index[0]][index[1]] = value;
    //}

    public bool checkSolved(Func<Solve, bool>[] rules)
    {
        foreach (Func<Solve, bool> rule in rules)
            if (!rule(this))
                return false;
        return true;
    }
}
