using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject gridObject;
    private Grid grid;

    private void Awake()
    {
        grid = gridObject.GetComponent<Grid>();
        grid.CreateGrid();
    }
}
