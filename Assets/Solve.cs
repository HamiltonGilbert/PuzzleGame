using System;
using System.Collections.Generic;
using UnityEngine;

public class Solve
{
    private GridData gridData;
    private Func<GridData, bool>[] rules;
    public int NumberOfRules { get => rules.Length; }
    private readonly LevelManager levelManager;

    public Solve(LevelData levelData, LevelManager levelManager)
    {
        bool?[][]tempGridState = new bool?[levelData.rows][];
        for (int i = 0; i < levelData.rows; i++)
            tempGridState[i] = new bool?[levelData.columns];
        gridData = new GridData(tempGridState, levelData.numberedTiles);

        rules = getRules(levelData);
        this.levelManager = levelManager;
    }

    public void MakeMove(int row, int column, bool? state)
    {
        gridData.UpdateGridState(row, column, state);
        bool[] results = CheckRules();
        levelManager.UpdateRules(results);
        levelManager.SetLevelCompleted(CheckCompleted(results));
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
    // TODO not needed once null tiles are just removed
    public bool CheckCompleted(bool[] results)
    {
        foreach (bool result in results)
            if (!result)
                return false;
        foreach (bool?[] column in gridData.gridState)
            foreach (bool? state in column)
                if (state == null)
                    return false;
        return true;
    }

    private Func<GridData, bool>[] getRules(LevelData levelData)
    {
        rules = new Func<GridData, bool>[levelData.ruleNames.Length];
        for (int i = 0; i < rules.Length; i++)
        {
            rules[i] = (Func<GridData, bool>)Delegate.CreateDelegate(typeof(Func<bool?[][], bool>), typeof(Rules).GetMethod(levelData.ruleNames[i].ToString()));
        }
        return rules;
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