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

    /// <summary>
    /// Creates a new Cell object.
    /// </summary>
    /// <param name="x">The X coordinate of the cell in the grid its in.</param>
    /// <param name="y">The Y coordinate of the cell in the grid its in.</param>
    /// <param name="alive">The living state of the cell. True to alive, false to dead.</param>
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
    public int LivingNeighborCount(Cell[,] grid)
    {
        int livingCount = 0;

        // Checks the cells surrounding this cell. Any cell with X coordinate X - 1 is to the left
        // of the cell, X is the top or bottom of the cell, and X + 1 is to the right of the cell.
        // Similarly, any cell with Y coordinate Y - 1 is below the cell, Y is the same level as
        // this cell, and Y + 1 is above the cell. Thus, (X - 1, Y - 1) is the bottom left cell,
        // (X + 1, Y + 1) is the top right cell, etc.
        for (int x = X - 1; x < X + 2; x++)
        {
            for (int y = Y - 1; y < Y + 2; y++)
            {
                if (x == X && y == Y)
                {
                    continue;
                }

                if (IsValidCoordinate(grid, x, y) && grid[x, y].Alive)
                {
                    livingCount++;
                }
            }
        }

        return livingCount;
    }

    /// <summary>
    /// Checks if a given (x, y) coordinate is a valid grid coordinate or not.
    /// </summary>
    /// <param name="grid">The current state of the grid.</param>
    /// <param name="x">The x coordinate to check.</param>
    /// <param name="y">The y coordinate to check.</param>
    /// <returns>True if the (x, y) coordinate is valid in the grid or not.</returns>
    private bool IsValidCoordinate(Cell[,] grid, int x, int y)
    {
        int gridWidth = grid.GetLength(0);
        int gridHeight = grid.GetLength(1);
        return x >= 0 && x < gridWidth && y >= 0 && y < gridHeight;
    }
}
