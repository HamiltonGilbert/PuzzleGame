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
    [SerializeField] private Color pressedColor;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    private bool defaultState = false;
    private bool state;

    public void CreateTile(int row, int column, Grid grid)
    {
        this.grid = grid;
        index[0] = row;
        index[1] = column;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        image.color = pressedColor;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        SwitchState();
    }

    public void SetState(bool state)
    {
        this.state = state;
    }
    public void SwitchState()
    {
        state = !state;
        UpdateColor();
    }
    public void ResetState()
    {
        state = defaultState;
    }
    private void UpdateColor()
    {
        if (state)
            image.color = activeColor;
        else
            image.color = inactiveColor;
    }
}
