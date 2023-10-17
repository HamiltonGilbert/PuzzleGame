using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Tile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private int[] index = new int[2];
    private Grid grid;
    [SerializeField] private Image image;
    [SerializeField] private GameObject fixedimage;
    [SerializeField] private Color pressedColor;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    private bool defaultState = false;
    private bool state;
    private bool isFixed = false;

    public void CreateTile(int row, int column, Grid grid)
    {
        index[0] = row;
        index[1] = column;
        this.grid = grid;
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
        UpdateColor();
    }

    //private void SetState(bool state)
    //{
    //    this.state = state;
    //    UpdateColor();
    //}
    private void SwitchState()
    {
        state = !state;
        UpdateColor();
    }
    public void ResetState()
    {
        if (!isFixed)
        {
            state = false;
            UpdateColor();
        }
    }
    private void UpdateColor()
    {
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
