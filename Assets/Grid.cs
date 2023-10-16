using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Color pressedColor;
    [SerializeField] private Color releasedColor;

    public int rows;
    public int columns;

    private GameObject[][] tileObjects;

    public void TilePressed(int[] index)
    {
        tileObjects[index[0]][index[1]].GetComponent<Tile>().SetColor(pressedColor);
    }
    public void TileReleased(int[] index)
    {
        tileObjects[index[0]][index[1]].GetComponent<Tile>().SetColor(releasedColor);
    }

    public void CreateGrid(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
        int tileDimensions = TileDimensions(rows, columns);
        float tileScale = tileDimensions / 100f;
        Debug.Log("tile dimensions: " + tileDimensions);
        int xPos = (columns * -1 * tileDimensions / 2) + tileDimensions / 2;
        int yPos = (rows * -1 * tileDimensions / 2) + tileDimensions / 2;
        tileObjects = new GameObject[rows][];
        tileObjects = new GameObject[rows][];
        for (int r = 0; r < rows; r++)
        {
            GameObject[] rowObjects = new GameObject[columns];
            for (int c = 0; c < columns; c++)
            {
                GameObject tempTile = Instantiate(tilePrefab, gameObject.transform);
                tempTile.transform.position = new Vector3(transform.position.x + xPos + (tileDimensions * c),transform.position.y + yPos + (tileDimensions * r), 0);
                tempTile.transform.localScale = new Vector3(tileScale, tileScale, 1f);
                tempTile.GetComponent<Tile>().CreateTile(r, c, this);
                rowObjects[c] = tempTile;
            }
            tileObjects[r] = rowObjects;
        }
    }

    public void ResetGrid()
    {
        foreach (GameObject[] row in tileObjects)
        {
            foreach (GameObject tile in row)
            {
                tile.GetComponent<Tile>().ResetColor();
            }
        }
    }

    public int TileDimensions(int rows, int columns)
    {
        // Replace with canvas-based sizing
        int width = 1120 / columns;
        int height = 632 / rows;
        if (width < height)
            return width;
        return height;
    }
}
