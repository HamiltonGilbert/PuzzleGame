using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private GridData gridData;
    [SerializeField] private Image[] ruleImages;
    [SerializeField] private Color incompleteColor = new Color(50, 255, 50, 100);
    [SerializeField] private Color completeColor = new Color(50, 255, 50, 100);
    private Solve solve;

    public void Start()
    {
        solve = new Solve(gridData, this);
        grid.CreateGrid(gridData, solve);
        UpdateRules(solve.CheckRules());
    }

    public void UndoMove()
    {
        grid.ResetGrid();
    }

    public void LevelComplete()
    {
        //gameObject.SetActive(false);
    }

    public void UpdateRules(bool[] results)
    {
        for (int i = 0; i < ruleImages.Length; i++)
        {
            if (results[i])
                ruleImages[i].color = completeColor;
            else
                ruleImages[i].color = incompleteColor;
        }
    }
}
