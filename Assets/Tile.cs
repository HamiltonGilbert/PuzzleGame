using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Tile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    private int row;
    private int column;
    private Solve solve;
    [SerializeField] private Image image;
    [SerializeField] private GameObject fixedimage;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    private bool state = false;
    private bool isFixed = false;
    private bool isNumbered = false;

    public void CreateTile(int row, int column, Solve solve)
    {
        this.row = row;
        this.column = column;
        this.solve = solve;
        UpdateTile();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        bool? newState = null;
        if (!isFixed)
            if (eventData.button == PointerEventData.InputButton.Left && state != true)
                newState = true;
            else if (eventData.button == PointerEventData.InputButton.Right && state != false)
                newState = false;
        if (newState != null)
        {
            SetState((bool)newState);
            UpdateTile();
            solve.MakeMove();
        }
        
    }
    // TODO
    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (!isFixed)
        //    if (eventData.button == PointerEventData.InputButton.Left)
        //        SetState(true);
        //    else if (eventData.button == PointerEventData.InputButton.Right)
        //        SetState(false);
    }

    public void SetFixed(bool state)
    {
        this.state = state;
        isFixed = true;
        fixedimage.SetActive(true);
        UpdateTile();
    }
    public void SetNumber(int number)
    {
        isNumbered = true;
        text.text = number.ToString();
        text.gameObject.SetActive(true);
        UpdateTile();
    }

    //private void SetState(bool state)
    //{
    //    this.state = state;
    //    UpdateColor();
    //}
    private void SetState(bool newState)
    {
        state = newState;
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
        solve.SetState(row, column, state);
        if (state)
            image.color = activeColor;
        else
            image.color = inactiveColor;
        if (isNumbered)
            text.color = text.color = new Color(1f - image.color.r, 1f - image.color.g, 1f - image.color.b, 1f);
    }
}

//public enum State
//{
//    True,
//    False,
//    NULL

//}

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
