using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridCreator : MonoBehaviour
{
    [SerializeField] private Grid grid;
    //[SerializeField] private TMP_InputField rowInput;
    public int rows;
    public int columns;

    public void SetRows(string rowText)
    {
        rows = Int32.Parse(rowText);
    }
    public void SetColumns(string columnText)
    {
        columns = Int32.Parse(columnText);
    }
    public void CreateGrid()
    {
        Debug.Log("creating grid with " + rows + " rows and " + columns + " columns");
        grid.CreateGrid(rows, columns);
    }
}
