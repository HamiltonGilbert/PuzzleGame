using System;
using System.Collections.Generic;

public static class Rules
{
    public enum RuleName { HasFiveBlack, NoFourInARow };
    // make these take in a tile instead and have each tile keep information about neighbor tiles?
    //         then it can stop you from making incorrect moves in real time
    //public static void AllBlackConnected(Solve solve)
    //{
    //    int rows = solve.rows;
    //    int columns = grid.columns;
    //    for ()
    //}

    public static bool HasFiveBlack(Solve solve)
    {
        int blackTiles = 0;
        bool[][] gridState = solve.gridState;
        for (int r = 0; r < gridState.Length; r++)
        {
            for (int c = 0; c < gridState.Length; c++)
            {
                if (gridState[r][c])
                    blackTiles++;
                if (blackTiles == 5)
                    return true;
            }
        }
        return false;
    }
    public static bool NoFourInARow(Solve solve)
    {
        return true;
    }
}
