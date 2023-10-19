using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Button UndoMoveBtn;
    [SerializeField] private GridData gridData;
    [SerializeField] private Image[] ruleImages;
    [SerializeField] private Color incompleteColor = new Color(50, 255, 50, 100);
    [SerializeField] private Color completeColor = new Color(50, 255, 50, 100);
    private Solve solve;

    public void Start()
    {
        gameOverScreen.SetActive(false);
        solve = new Solve(gridData, this);
        grid.CreateGrid(gridData, solve);
        HideRules();
        UpdateRules(solve.CheckRules());
    }

    public void UndoMove()
    {
        grid.ResetGrid();
    }

    public void LevelComplete()
    {
        grid.EndLevel();
        gameOverScreen.SetActive(true);
        UndoMoveBtn.interactable = false;
    }

    public void UpdateRules(bool[] results)
    {
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i])
                ruleImages[i].color = completeColor;
            else
                ruleImages[i].color = incompleteColor;
        }
    }
    public void HideRules()
    {
        for (int i = ruleImages.Length; i > solve.NumberOfRules; i--)
        {
            ruleImages[i - 1].color = new Color (0, 0, 0, 0);
        }
    }
}
