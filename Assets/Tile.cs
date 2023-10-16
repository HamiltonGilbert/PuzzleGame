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

    private Color defaultColor = new Color(255, 255, 255, 255);

    public void CreateTile(int row, int column, Grid grid)
    {
        this.grid = grid;
        index[0] = row;
        index[1] = column;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        grid.TilePressed(index);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        grid.TileReleased(index);
    }

    public void SetColor(Color color)
    {
        image.color = color;
    }
    public void ResetColor()
    {
        image.color = defaultColor;
    }
}
