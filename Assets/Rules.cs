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
        return true;
    }
    public static bool NoFourInARow(Solve solve)
    {
        return true;
    }
}
