using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject gridPrefab;
    private GameObject gridObject;
    private Grid grid;

    private void Awake()
    {
        grid = Instantiate(gridPrefab, gameObject.transform).GetComponent<Grid>();
        grid.CreateGrid();
    }

    public void UndoMove()
    {
        grid.ResetGrid();
    }
}
