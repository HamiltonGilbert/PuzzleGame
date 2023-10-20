using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Button undoMoveBtn;
    [SerializeField] private Button nextLvlButton;
    [SerializeField] private Button backLvlButton;
    [SerializeField] private LevelData[] levelData;
    [SerializeField] private Image[] ruleImages;
    [SerializeField] private Color incompleteColor = new Color(50, 255, 50, 100);
    [SerializeField] private Color completeColor = new Color(50, 255, 50, 100);
    private Solve solve;
    private int currentLevel = 0;
    //private int levelsCompleted = 0;

    public void Start() { SetLevel(currentLevel); }
    public void UndoMove() { grid.ResetGrid(); }
    public void NextLvlBtnPressed() { SetLevel(currentLevel + 1); }
    public void BackLvlBtnPressed() { SetLevel(currentLevel - 1); }

    public void SetLevelCompleted(bool state)
    {
        //if (currentLevel > levelsCompleted)
        //    levelsCompleted = currentLevel;
        //grid.EndLevel();
        if (currentLevel < levelData.Length - 1)
            nextLvlButton.interactable = state;
        else
            EndGame();
        
        //undoMoveBtn.interactable = false;
    }
    public void SetLevel(int level)
    {
        if (level >= levelData.Length)
        {
            Debug.Log("level " + level + " is out of bounds" + levelData.Length);
            return;
        }
        //if (level > levelsCompleted)
        //    nextLvlButton.interactable = false;
        nextLvlButton.interactable = false;
        if (level == 0)
            backLvlButton.interactable = false;
        else
            backLvlButton.interactable = true;
        undoMoveBtn.interactable = true;
        solve = new Solve(levelData[level], this);
        grid.CreateGrid(levelData[level], solve);
        HideRules();
        UpdateRules(solve.CheckRules());
        currentLevel = level;
    }
    public void EndGame()
    {
        // TODO
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
