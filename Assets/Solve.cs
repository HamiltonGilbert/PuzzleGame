using System;
using System.Collections.Generic;
using UnityEngine;

public class Solve
{
    private GridData gridData;
    private Func<GridData, bool>[] rules;
    public int NumberOfRules { get => rules.Length; }
    private readonly LevelManager levelManager;

    public Solve(LevelData levelData, LevelManager levelManager, GridData gridData)
    {
        this.levelManager = levelManager;
        this.gridData = gridData;
        rules = GetRules(levelData);
        levelManager.UpdateRules(CheckRules());
    }

    public void MakeMove(int row, int column, bool state)
    {
        gridData.UpdateTileState(row, column, state);
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
        rules = new Func<GridData, bool>[levelData.ruleNames.Length];
        for (int i = 0; i < rules.Length; i++)
        {
            rules[i] = (Func<GridData, bool>)Delegate.CreateDelegate(typeof(Func<GridData, bool>), typeof(Rules).GetMethod(levelData.ruleNames[i].ToString()));
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
                else
                    temp += "|| " + tileState.ToString() + " ";
            }
            result += temp + "||\n";
        }
        Debug.Log(result);
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