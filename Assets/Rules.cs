using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using static Helpers;

public static class Rules
{
    public enum RuleName { HasFiveBlack, LessThanSevenBlack, LessThanSevenWhite, NoSingleBlack,
        AllBlackConnected, NoFourInARowBlack, NoThreeInADiagonalBlack,
        SameNumbersConnected, NumbersAreaSize
    };

    // COLOR AMOUNT
    public static bool HasFiveBlack(GridData gridData)
    {
        int blackTiles = 0;
        for (int r = 0; r < gridData.gridState.Length; r++)
        {
            for (int c = 0; c < gridData.gridState[r].Length; c++)
            {
                if (gridData.gridState[r][c] != null)
                {
                    if ((bool)gridData.gridState[r][c])
                        blackTiles++;
                    if (blackTiles == 5)
                        return true;
                }
            }
        }
        return false;
    }
    public static bool LessThanSevenBlack(GridData gridData)
    {
        int blackTiles = 0;
        for (int r = 0; r < gridData.gridState.Length; r++)
        {
            for (int c = 0; c < gridData.gridState[r].Length; c++)
            {
                if (gridData.gridState[r][c] != null)
                {
                    if ((bool)gridData.gridState[r][c])
                        blackTiles++;
                }
            }
        }
        if (blackTiles < 7)
            return true;
        return false;
    }
    public static bool LessThanSevenWhite(GridData gridData)
    {
        int whiteTiles = 0;
        for (int r = 0; r < gridData.gridState.Length; r++)
        {
            for (int c = 0; c < gridData.gridState[r].Length; c++)
            {
                if (gridData.gridState[r][c] != null)
                {
                    if (!(bool)gridData.gridState[r][c])
                        whiteTiles++;
                }
            }
        }
        if (whiteTiles < 7)
            return true;
        return false;
    }
    public static bool NoSingleBlack(GridData gridData)
    {
        for (int r = 0; r < gridData.gridState.Length; r++)
        {
            for (int c = 0; c < gridData.gridState[r].Length; c++)
            {
                if (gridData.gridState[r][c] != null)
                {
                    if ((bool)gridData.gridState[r][c])
                    {
                        List<int[]> neighbors = GetViableNeighbors(gridData.gridState, new int[] { r, c });
                        bool isBlack = false;
                        foreach (int[] neighbor in neighbors)
                        {
                            if (gridData.gridState[neighbor[0]][neighbor[1]] != null)
                                if ((bool)gridData.gridState[neighbor[0]][neighbor[1]])
                                    isBlack = true;
                        }
                        if (!isBlack)
                            return false;
                    }
                }
            }
        }
        return true;
    }
    // BLACK CONNECTED
    public static bool AllBlackConnected(GridData gridData)
    {
        // get black tile to start
        int[] startTile = null;
        for (int r = 0; r < gridData.gridState.Length; r++)
        {
            for (int c = 0; c < gridData.gridState[r].Length; c++)
                if (gridData.gridState[r][c] != null)
                    if (gridData.gridState[r][c] == true)
                    {
                        startTile = new int[] { r, c };
                        break;
                    }
            if (startTile != null)
                break;
        }
        if (startTile == null)
            return true;

        Stack<int[]> toVisit = new();
        toVisit.Push(startTile);
        List<int[]> visited = new();

        // visit all black connected to start tile
        int[] currentIndex;
        while (toVisit.Count != 0)
        {
            currentIndex = toVisit.Pop();
            visited.Add(currentIndex);
            foreach (int[] index in GetViableNeighbors(gridData.gridState, currentIndex))
                if (!visited.Any(p => p.SequenceEqual(index)) && gridData.gridState[index[0]][index[1]] == true)
                    toVisit.Push(index);
        }

        // check for black not visited
        for (int r = 0; r < gridData.gridState.Length; r++)
        {
            for (int c = 0; c < gridData.gridState[r].Length; c++)
                if (gridData.gridState[r][c] != null)
                    if (gridData.gridState[r][c] == true)
                        if (!visited.Any(p => p.SequenceEqual(new int[] { r, c })))
                        {
                            return false;
                        }
        }
        return true;
    }
    public static bool NoFourInARowBlack(GridData gridData)
    {
        // replace once general functions that take an input are introduced
        int numberInARow = 4;

        // horizontal
        if (gridData.gridState[0].Length > numberInARow)
            for (int r = 0; r < gridData.gridState.Length; r++)
            {
                for (int c = 0; c < gridData.gridState[r].Length - (numberInARow - 1); c++)
                {
                    if (gridData.gridState[r][c] != null)
                    {
                        if ((bool)gridData.gridState[r][c])
                        {
                            bool result = true;
                            int[][] tilesToCheck = GetTilesInDirection(new int[] { r, c }, new int[] { 0, 1 }, (numberInARow - 1));
                            //int[][] tilesToCheck = { new int[] { r, c + 1 }, new int[] { r, c + 2 }, new int[] { r, c + 3 } };
                            if (AreViableTiles(gridData.gridState, tilesToCheck))
                                foreach (int[] tile in tilesToCheck)
                                    result &= (bool)gridData.gridState[tile[0]][tile[1]];
                            else
                                result = false;
                            if (result)
                                return false;
                        }
                        
                    }
                }
            }
        //vertical
        if (gridData.gridState.Length > numberInARow)
            for (int r = 0; r < gridData.gridState.Length - (numberInARow - 1); r++)
            {
                for (int c = 0; c < gridData.gridState[r].Length; c++)
                {
                    if (gridData.gridState[r][c] != null)
                    {
                        if ((bool)gridData.gridState[r][c])
                        {
                            bool result = true;
                            int[][] tilesToCheck = GetTilesInDirection(new int[] { r, c }, new int[] { 1, 0 }, (numberInARow - 1));
                            //int[][] tilesToCheck = { new int[] { r + 1, c }, new int[] { r + 2, c }, new int[] { r + 3, c } };
                            if (AreViableTiles(gridData.gridState, tilesToCheck))
                                foreach (int[] tile in tilesToCheck)
                                    result &= (bool)gridData.gridState[tile[0]][tile[1]];
                            else
                                result = false;
                            if (result)
                                return false;
                        }
                    }
                }
            }
        return true;
    }
    public static bool NoThreeInADiagonalBlack(GridData gridData)
    {
        // replace once general functions that take an input are introduced
        int numberInARow = 3;
        if (gridData.gridState.Length < numberInARow || gridData.gridState[0].Length < numberInARow)
        {
            Debug.Log("Grid not large enough for " + numberInARow + " length diagonal");
            return true;
        }
        // right up
        for (int r = 0; r < gridData.gridState.Length - (numberInARow - 1); r++)
        {
            for (int c = 0; c < gridData.gridState[r].Length - (numberInARow - 1); c++)
            {
                if (gridData.gridState[r][c] != null)
                {
                    if ((bool)gridData.gridState[r][c])
                    {
                        bool result = true;
                        int[][] tilesToCheck = GetTilesInDirection(new int[] { r, c }, new int[] { 1, 1 }, (numberInARow - 1));
                        //int[][] tilesToCheck = { new int[] { r + 1, c + 1 }, new int[] { r + 2, c + 2 } };
                        if (AreViableTiles(gridData.gridState, tilesToCheck))
                            foreach (int[] tile in tilesToCheck)
                                result &= (bool)gridData.gridState[tile[0]][tile[1]];
                        else
                            result = false;
                        if (result)
                            return false;
                    }

                }
            }
        }
        //right down
        for (int r = (numberInARow - 1); r < gridData.gridState.Length; r++)
        {
            for (int c = 0; c < gridData.gridState[r].Length - (numberInARow - 1); c++)
            {
                if (gridData.gridState[r][c] != null)
                {
                    if ((bool)gridData.gridState[r][c])
                    {
                        bool result = true;
                        int[][] tilesToCheck = GetTilesInDirection(new int[] { r, c }, new int[] { -1, 1 }, (numberInARow - 1));
                        //int[][] tilesToCheck = { new int[] { r + -1, c + 1 }, new int[] { r + -2, c + 2 } };
                        if (AreViableTiles(gridData.gridState, tilesToCheck))
                            foreach (int[] tile in tilesToCheck)
                                result &= (bool)gridData.gridState[tile[0]][tile[1]];
                        else
                            result = false;
                        if (result)
                            return false;
                    }
                }
            }
        }
        return true;
    }
    // NUMBERS
    public static bool SameNumbersConnected(GridData gridData)
    {
        int[][] numberedTiles = gridData.numberedTilesIndices;

        Stack<int[]> toVisit = new();
        toVisit.Push(numberedTiles[0]);
        List<int[]> visited = new();

        // TODO temp only first numbered tile
        int[] currentNumbered = numberedTiles[0];
        
        // get list of same numbered tiles
        int number = currentNumbered[2];
        List<int[]> IndicesOfNumber = new();
        foreach (int[] tile in numberedTiles)
            if (tile[2] == number)
                IndicesOfNumber.Add(new int[] { tile[0], tile[1] });

        // visit all with the same state connected to start tile until other of number are found
        bool state = (bool)gridData.gridState[currentNumbered[0]][currentNumbered[1]];
        int[] currentIndex;
        int numberConnected = 0;
        while (toVisit.Count != 0)
        {
            currentIndex = toVisit.Pop();
            // if index is the same number
            if (!IndicesOfNumber.Any(p => p.SequenceEqual(currentIndex)))
                numberConnected++;
            if (numberConnected >= IndicesOfNumber.Count)
            {
                Debug.Log(numberConnected);
                return true;
            }

            visited.Add(currentIndex);
            foreach (int[] index in GetViableNeighbors(gridData.gridState, currentIndex))
                if (!visited.Any(p => p.SequenceEqual(index)) && gridData.gridState[index[0]][index[1]] == state)
                    toVisit.Push(index);
        }
        return false;
    }
    public static bool NumbersAreaSize(GridData gridData)
    {
        return true;
    }
}


public static class Helpers
{
    public static List<int[]> GetViableNeighbors(bool?[][] gridState, int[] tileIndex)
    {
        List<int[]> viableNeighbors = new List<int[]>();
        int[][] possibleNeighbors = new int[][] { new int[] {tileIndex[0] + 1, tileIndex[1]}, new int[] { tileIndex[0] - 1, tileIndex[1] }, new int[] { tileIndex[0], tileIndex[1] + 1 }, new int[] { tileIndex[0], tileIndex[1] - 1 } };

        foreach (int[] possibleNeighbor in possibleNeighbors)
            if (possibleNeighbor[0] < gridState.Length && possibleNeighbor[0] >= 0 && possibleNeighbor[1] < gridState[tileIndex[0]].Length && possibleNeighbor[1] >= 0)
                if (gridState[possibleNeighbor[0]][possibleNeighbor[1]] != null)
                    viableNeighbors.Add(possibleNeighbor);
        return viableNeighbors;
    }
    public static bool IsViableTile(bool?[][] gridState, int[] tileIndex)
    {
        if (tileIndex[0] < gridState.Length && tileIndex[0] >= 0 && tileIndex[1] < gridState[tileIndex[0]].Length && tileIndex[1] >= 0)
            if (gridState[tileIndex[0]][tileIndex[1]] != null)
                return true;
        return false;
    }
    public static bool AreViableTiles(bool?[][] gridState, int[][] tileIndexes)
    {
        bool result = true;
        foreach (int[] tileIndex in tileIndexes)
            result &= IsViableTile(gridState, tileIndex);
        return result;
    }
    public static int[][] GetTilesInDirection(int[] startTileIndex, int[] direction, int numberWanted)
    {
        int[][] tiles = new int[numberWanted][];
        for (int i = 0; i < numberWanted; i++)
            tiles[i] = new int[] { startTileIndex[0] + (direction[0] * (i + 1)), startTileIndex[1] + (direction[1] * (i + 1)) };
        return tiles;
    }
}