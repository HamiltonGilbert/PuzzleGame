using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    public Color hitColor;

    private GameObject[][] tileObjects;

    public void TilePressed(int[] index)
    {
        tileObjects[index[0]][index[1]].GetComponent<Tile>().SetColor(hitColor);
    }
    public void TileReleased(int[] index)
    {
        tileObjects[index[0]][index[1]].GetComponent<Tile>().ResetColor();
    }

    public void CreateGrid(int rows, int columns)
    {
        int xPos = (columns * -50) + 50;
        int yPos = (rows * -50)+ 50;
        tileObjects = new GameObject[rows][];
        tileObjects = new GameObject[rows][];
        for (int r = 0; r < rows; r++)
        {
            GameObject[] rowObjects = new GameObject[columns];
            for (int c = 0; c < rows; c++)
            {
                Debug.Log("created object at " + r + ":" + c);
                GameObject tempTile = Instantiate(tilePrefab, gameObject.transform);
                tempTile.transform.position = new Vector3(transform.position.x + xPos + (100 * c),transform.position.y + yPos + (100 * r), 0);
                tempTile.GetComponent<Tile>().SetValues(r, c, this);
                rowObjects[c] = tempTile;
            }
            tileObjects[r] = rowObjects;
        }
    }
}
