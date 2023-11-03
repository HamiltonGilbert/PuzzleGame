using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using static Helpers;

public static class Rules
{
    public enum RuleName { HasFiveBlack, LessThanSevenBlack, LessThanSevenWhite, NoSingleBlack,
        AllBlackConnected, NoFourInARowBlack, NoThreeInARowBlack, NoThreeInADiagonalBlack,
        SameNumbersConnected, NoDifferentNumbersConnected, NumbersAreaSize
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
                            if (gridData.GetState(neighbor) != null)
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

        if (gridData.gridState.Length < numberInARow || gridData.gridState[0].Length < numberInARow)
        {
            Debug.Log("Grid not large enough for " + numberInARow + " in a row");
            return false;
        }

        // horizontal
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
                                result &= (bool)gridData.GetState(tile);
                        else
                            result = false;
                        if (result)
                            return false;
                    }
                }
            }
        }
        //vertical
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
                                result &= (bool)gridData.GetState(tile);
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
    public static bool NoThreeInARowBlack(GridData gridData)
    {
        // replace once general functions that take an input are introduced
        int numberInARow = 3;

        if (gridData.gridState.Length < numberInARow || gridData.gridState[0].Length < numberInARow)
        {
            Debug.Log("Grid not large enough for " + numberInARow + " in a row");
            return false;
        }
        // horizontal
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
                                result &= (bool)gridData.GetState(tile);
                        else
                            result = false;
                        if (result)
                            return false;
                    }
                }
            }
        }
        //vertical
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
                                result &= (bool)gridData.GetState(tile);
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
            return false;
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
                        int[][] tilesToCheck = GetTilesInDirection(new int[] { r, c }, new int[] { 1, 1 }, numberInARow - 1);
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
                        int[][] tilesToCheck = GetTilesInDirection(new int[] { r, c }, new int[] { -1, 1 }, numberInARow - 1);
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
        List<int[]> numberedTiles = new();
        foreach (int[] index in GetIndices(gridData.numberedTilesIndices))
            numberedTiles.Add(index);

        while (numberedTiles.Count != 0)
        {
            List<int[]> currentNumberedTiles = numberedTiles;
            Stack<int[]> toVisit = new();
            List<int[]> visited = new();
            List<int[]> IndicesOfNumber = new();
            // seperate index and number from numberedTile
            int[] currentNumberedTile = new int[] { currentNumberedTiles[0][0], currentNumberedTiles[0][1] };
            int goalNumber = currentNumberedTiles[0][2];
            toVisit.Push(currentNumberedTile);

            // get list of same numbered tiles
            numberedTiles = new();
            foreach (int[] tile in currentNumberedTiles)
                if (tile[2] == goalNumber)
                {   // remove number from index
                    IndicesOfNumber.Add(new int[] { tile[0], tile[1] });
                }
                else // get all other numbered tiles for next loop
                    numberedTiles.Add(tile);

            // visit all with the same state connected to start tile until others of number are found
            bool goalState = (bool)gridData.GetState(currentNumberedTile);
            int[] currentIndex;
            int numberConnected = 0;
            bool allFound = false;
            while (toVisit.Count != 0)
            {
                currentIndex = toVisit.Pop();
                // if index is the same number
                if (IndicesOfNumber.Any(p => p.SequenceEqual(currentIndex)))
                {
                    numberConnected++;
                    //Debug.Log($"index: ({currentIndex[0]}, {currentIndex[1]}) {numberConnected}");
                }
                //Debug.Log($"index: ({currentIndex[0]}, {currentIndex[1]}) {numberConnected}");
                if (numberConnected >= IndicesOfNumber.Count)
                {
                    allFound = true;
                    break;
                }

                visited.Add(currentIndex);
                foreach (int[] index in GetViableNeighbors(gridData.gridState, currentIndex))
                    if (!visited.Any(p => p.SequenceEqual(index)) && gridData.GetState(index) == goalState)
                        toVisit.Push(index);
            }
            if (!allFound) return false;
        }
        return true;
    }
    public static bool NoDifferentNumbersConnected(GridData gridData)
    {
        //return true;

        Dictionary<(int, int), int> numberedTileIndices = new();
        Stack<int[]> numberedTilesToCheck = new();
        foreach (int[] index in GetIndices(gridData.numberedTilesIndices))
        {
            numberedTileIndices.Add(( index[0], index[1] ), index[2]);
            numberedTilesToCheck.Push(index);
        }

        while (numberedTilesToCheck.Count != 0)
        {
            int[] numberedTile = numberedTilesToCheck.Pop();
            Stack<int[]> toVisit = new();
            List<int[]> visited = new();
            List<int[]> IndicesOfNumber = new();
            // seperate index and number from numberedTile
            int[] currentNumberedTile = new int[] { numberedTile[0], numberedTile[1] };
            int goalNumber = numberedTile[2];
            toVisit.Push(currentNumberedTile);

            // visit all with the same state connected to start tile until others of number are found
            bool goalState = (bool)gridData.GetState(currentNumberedTile);
            int[] currentIndex;
            while (toVisit.Count != 0)
            {
                currentIndex = toVisit.Pop();
                // not starting tile
                if (!currentIndex.SequenceEqual(currentNumberedTile))
                    // in dictionary of other numbered tiles
                    if (numberedTileIndices.TryGetValue((currentIndex[0], currentIndex[1]), out int number))
                        if (number != goalNumber)
                            return false;

                visited.Add(currentIndex);
                foreach (int[] index in GetViableNeighbors(gridData.gridState, currentIndex))
                    if (!visited.Any(p => p.SequenceEqual(index)) && gridData.GetState(index) == goalState)
                        toVisit.Push(index);
            }
        }
        return true;
    }
    public static bool NumbersAreaSize(GridData gridData)
    {
        Stack<int[]> numberedTiles = new();
        foreach (int[] index in GetIndices(gridData.numberedTilesIndices))
            numberedTiles.Push(index);

        while (numberedTiles.Count != 0)
        {
            Stack<int[]> toVisit = new();
            List<int[]> visited = new();
            int[] nextTile = numberedTiles.Pop();
            // seperate index and number from numberedTile
            int[] currentNumberedTile = new int[]{nextTile[0], nextTile[1]};
            int goalNumber = nextTile[2];
            toVisit.Push(currentNumberedTile);

            bool goalState = (bool)gridData.GetState(currentNumberedTile);
            int[] currentIndex;
            int numberConnected = 0;
            while (toVisit.Count != 0)
            {
                numberConnected++;
                currentIndex = toVisit.Pop();

                if (numberConnected > goalNumber)
                    return false;

                visited.Add(currentIndex);
                foreach (int[] index in GetViableNeighbors(gridData.gridState, currentIndex))
                    if (!visited.Any(p => p.SequenceEqual(index)) && !toVisit.Any(p => p.SequenceEqual(index)) && gridData.GetState(index) == goalState)
                        toVisit.Push(index);
            }
            if (numberConnected != goalNumber)
                return false;
                
        }
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
    public static int[][] GetIndices(int[][] nonIndexed)
    {
        int[][] indices = new int[nonIndexed.Length][];
        for (int i = 0; i < nonIndexed.Length; i++)
        {
            indices[i] = new int[nonIndexed[i].Length];
            // row, columns
            indices[i][0] = nonIndexed[i][0] - 1;
            indices[i][1] = nonIndexed[i][1] - 1;
            // any other numbers in nonIndexed
            for (int j = 2; j < nonIndexed[i].Length; j++)
                indices[i][j] = nonIndexed[i][j];
        }
        return indices;
    }
    public static bool ArrayComparator(int[] x, int[] y)
    {
        if (x.Length != y.Length)
        {
            return false;
        }
        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] != y[i])
            {
                return false;
            }
        }
        return true;
    }
}