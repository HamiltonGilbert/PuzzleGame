using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/*
 * handles all button presses (switches level, undoes move)
 * initializes everything at each level start
 * is called by Solve at the beginning of level and after every move
*/

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
    private int currentLevelIndex = 0;
    //private int levelsCompleted = 0;

    public void Start() { SetLevel(currentLevelIndex); }
    public void UndoMove() { grid.ResetGrid(); }
    public void NextLvlBtnPressed() { SetLevel(currentLevelIndex + 1); }
    public void BackLvlBtnPressed() { SetLevel(currentLevelIndex - 1); }

    public void SetLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;
        if (levelIndex >= levelData.Length)
        {
            Debug.Log("level " + levelIndex + " is out of bounds" + levelData.Length);
            return;
        }
        //if (level > levelsCompleted)
        //    nextLvlButton.interactable = false;
        nextLvlButton.interactable = false;
        if (levelIndex == 0)
            backLvlButton.interactable = false;
        else
            backLvlButton.interactable = true;
        undoMoveBtn.interactable = true;

        grid.CreateGrid(levelData[levelIndex], this);
        HideRules();
    }
    public void EndGame()
    {
        // TODO
    }
    // is called by Solve at the beginning of level and after every move
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
        for (int i = ruleImages.Length; i > levelData[currentLevelIndex].ruleNames.Length; i--)
        {
            ruleImages[i - 1].color = new Color (0, 0, 0, 0);
        }
    }

    public void SetLevelCompleted(bool state)
    {
        //if (currentLevel > levelsCompleted)
        //    levelsCompleted = currentLevel;
        //grid.EndLevel();
        if (currentLevelIndex < levelData.Length - 1)
            nextLvlButton.interactable = state;
        else
            EndGame();

        //undoMoveBtn.interactable = false;
    }
}
