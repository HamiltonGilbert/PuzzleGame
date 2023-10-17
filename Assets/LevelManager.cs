using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private GridData gridData;

    private void Start()
    {
        grid.CreateGrid(gridData);
    }

    public void UndoMove()
    {
        grid.ResetGrid();
    }
}
