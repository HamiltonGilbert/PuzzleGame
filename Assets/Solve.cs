using System;
using System.Collections.Generic;
using UnityEngine;

public class Solve
{
    public bool?[][] gridState;
    private Func<bool?[][], bool>[] rules;
    public int NumberOfRules { get => rules.Length; }
    private LevelManager levelManager;

    public Solve(LevelData levelData, LevelManager levelManager)
    {
        gridState = new bool?[levelData.rows][];
        for (int i = 0; i < levelData.rows; i++)
            gridState[i] = new bool?[levelData.columns];
        rules = getRules(levelData);
        this.levelManager = levelManager;
    }

    public void MakeMove(int row, int column, bool? state)
    {
        gridState[row][column] = state;
        bool[] results = CheckRules();
        levelManager.UpdateRules(results);
        levelManager.SetLevelCompleted(CheckCompleted(results));
    }

    public bool[] CheckRules()
    {
        bool[] result = new bool[rules.Length];
        for (int i = 0; i < rules.Length; i++)
        {
            result[i] = rules[i](gridState);
        }
        return result;
    }
    public bool CheckCompleted(bool[] results)
    {
        foreach (bool result in results)
            if (!result)
                return false;
        foreach (bool?[] column in gridState)
            foreach (bool? state in column)
                if (state == null)
                    return false;
        return true;
    }

    private Func<bool?[][], bool>[] getRules(LevelData levelData)
    {
        rules = new Func<bool?[][], bool>[levelData.ruleNames.Length];
        for (int i = 0; i < rules.Length; i++)
        {
            rules[i] = (Func<bool?[][], bool>)Delegate.CreateDelegate(typeof(Func<bool?[][], bool>), typeof(Rules).GetMethod(levelData.ruleNames[i].ToString()));
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