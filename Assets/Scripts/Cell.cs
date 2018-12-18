/// <summary>
/// The representation of a cell in a grid.
/// </summary>
public class Cell
{
    /// <summary>
    /// The X coordiante of this cell in the grid.
    /// </summary>
    public int X { get; private set; }

    /// <summary>
    /// The Y coordinate of this cell in the grid.
    /// </summary>
    public int Y { get; private set; }

    /// <summary>
    /// The current living state of this cell. True if this cell is living, false otherwise.
    /// </summary>
    public bool Alive { get; set; }

    public Cell(int x, int y, bool alive)
    {
        X = x;
        Y = y;
        Alive = alive;
    }

    /// <summary>
    /// Gets the number of living cells that are neighbors to this cell.
    /// </summary>
    /// <param name="grid">The current state of the grid.</param>
    /// <returns>The number of living cells that are neighbors to this cell.</returns>
    public int LivingCellCount(Cell[,] grid)
    {
        int livingNeighborCount = LivingCellSides(grid) + LivingCellAdjacentLeft(grid) + LivingCellAdjacentRight(grid);
        return livingNeighborCount;
    }

    /// <summary>
    /// Gets the number of living cells on the sides of the cell.
    /// </summary>
    /// <param name="grid">The current state of the grid.</param>
    /// <returns>The number of living cells on the sides of the cell.</returns>
    private int LivingCellSides(Cell[,] grid)
    {
        int sideCount = 0;

        // Check right side. If we reach the width of the grid, check opposite side of the grid.
        if (X + 1 <= grid.GetLength(0))
        {
            sideCount = grid[X, Y].Alive ? sideCount + 1 : sideCount;
        }
        else if (grid[0, Y].Alive)
        {
            sideCount++;
        }

        // Check left side. If x is less than 0, check opposite side of the grid.
        if (X - 1 >= 0)
        {
            sideCount = grid[X - 1, Y].Alive ? sideCount + 1 : sideCount;
        }
        else if (grid[grid.GetLength(0) - 1, Y].Alive)
        {
            sideCount++;
        }

        // Check top side. If we reach height of the grid, check bottom of the grid.
        if (Y + 1 <= grid.GetLength(1))
        {
            sideCount = grid[X, Y + 1].Alive ? sideCount + 1 : sideCount;
        }
        else if (grid[X, 0].Alive)
        {
            sideCount++;
        }

        // Check bottom side. If y is less than 0, check top of the grid.
        if (Y - 1 >= 0 && grid[X, Y - 1].Alive)
        {
            sideCount = grid[X, Y - 1].Alive ? sideCount + 1 : sideCount;
        }
        else if (grid[X, grid.GetLength(1) - 1].Alive)
        {
            sideCount++;
        }

        return sideCount;
    }

    /// <summary>
    /// Gets the number of living cells left adjacent (top left and bottom left) to this cell.
    /// </summary>
    /// <param name="grid">The current state of the grid.</param>
    /// <returns>The number of living cells left adjacent (top left and bottom left) to this cell.</returns>
    private int LivingCellAdjacentLeft(Cell[,] grid)
    {
        int adjLeftCount = 0;

        if (X - 1 >= 0)
        {
            // Case where the x coordinate before of this one is still a valid grid position.

            if (Y + 1 <= grid.GetLength(1))
            {
                adjLeftCount = grid[X - 1, Y + 1].Alive ? adjLeftCount + 1 : adjLeftCount;
            }
            else if (grid[X - 1, 0].Alive)
            {
                adjLeftCount++;
            }

            if (Y - 1 >= 0)
            {
                adjLeftCount = grid[X - 1, Y - 1].Alive ? adjLeftCount + 1 : adjLeftCount;
            }
            else if (grid[X - 1, grid.GetLength(1) - 1].Alive)
            {
                adjLeftCount++;
            }
        }
        else
        {
            // Case where the x coordinate before of this one aren't valid grid positions, thus we
            // wrap around and we use the width of the grid as the x coordinate.

            if (Y + 1 <= grid.GetLength(1))
            {
                adjLeftCount = grid[grid.GetLength(0) - 1, Y + 1].Alive ? adjLeftCount + 1 : adjLeftCount;
            }
            else if (grid[grid.GetLength(0) - 1, 0].Alive)
            {
                adjLeftCount++;
            }

            if (Y - 1 >= 0)
            {
                adjLeftCount = grid[grid.GetLength(0) - 1, Y - 1].Alive ? adjLeftCount + 1 : adjLeftCount;
            }
            else if (grid[grid.GetLength(0) - 1, grid.GetLength(1) - 1].Alive)
            {
                adjLeftCount++;
            }
        }

        return adjLeftCount;
    }

    /// <summary>
    /// Gets the number of living cells adjacent right (top right and bottom right) to this cell.
    /// </summary>
    /// <param name="grid">The current state of the grid.</param>
    /// <returns>The number of living cells adjacent right (top right and bottom right) to this cell.</returns>
    private int LivingCellAdjacentRight(Cell[,] grid)
    {
        int adjRightCount = 0;

        if (X + 1 <= grid.GetLength(0))
        {
            // Case where the x coordinate ahead of this one is still a valid grid position.

            if (Y + 1 <= grid.GetLength(1))
            {
                adjRightCount = grid[X + 1, Y + 1].Alive ? adjRightCount + 1 : adjRightCount;
            }
            else if (grid[X + 1, 0].Alive)
            {
                adjRightCount++;
            }

            if (Y - 1 >= 0)
            {
                adjRightCount = grid[X + 1, Y - 1].Alive ? adjRightCount + 1 : adjRightCount;
            }
            else if (grid[X + 1, grid.GetLength(1) - 1].Alive)
            {
                adjRightCount++;
            }
        }
        else
        {
            // Case where the x coordinate ahead of this one aren't valid grid positions, thus we
            // wrap around and we use the 0 x coordinate.

            if (Y + 1 <= grid.GetLength(1))
            {
                adjRightCount = grid[0, Y + 1].Alive ? adjRightCount + 1 : adjRightCount;
            }
            else if (grid[0, 0].Alive)
            {
                adjRightCount++;
            }

            if (Y - 1 >= 0)
            {
                adjRightCount = grid[0, Y - 1].Alive ? adjRightCount + 1 : adjRightCount;
            }
            else if (grid[0, grid.GetLength(1) - 1].Alive)
            {
                adjRightCount++;
            }
        }

        return adjRightCount;
    }
}
