using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Tile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private int row;
    private int column;
    private Solve solve;
    [SerializeField] private Image image;
    [SerializeField] private GameObject fixedimage;
    [SerializeField] private Color pressedColor;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    private bool defaultState = false;
    private bool state;
    private bool isFixed = false;

    public void CreateTile(int row, int column, Solve solve)
    {
        this.row = row;
        this.column = column;
        this.solve = solve;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isFixed)
            image.color = pressedColor;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isFixed)
            SwitchState();
    }

    public void SetFixed(bool state)
    {
        this.state = state;
        isFixed = true;
        fixedimage.SetActive(true);
        UpdateTile();
    }

    //private void SetState(bool state)
    //{
    //    this.state = state;
    //    UpdateColor();
    //}
    private void SwitchState()
    {
        state = !state;
        UpdateTile();
    }
    public void ResetState()
    {
        if (!isFixed)
        {
            state = false;
            UpdateTile();
        }
    }
    private void UpdateTile()
    {
        solve.MakeMove(row, column, state);
        if (state)
            image.color = activeColor;
        else
            image.color = inactiveColor;
    }
}

//public class FixedTile : Tile
//{
//    private int[] index = new int[2];
//    private Grid grid;
//    [SerializeField] private Image image;
//    [SerializeField] private Color activeColor;
//    [SerializeField] private Color inactiveColor;
//    private bool state;

//    public void CreateTile(int row, int column, bool isFixedTo, Grid grid)
//    {
//        index[0] = row;
//        index[1] = column;
//        state = isFixedTo;
//        this.grid = grid;
//        if (state)
//            image.color = activeColor;
//        else
//            image.color = inactiveColor;
//    }
//}
