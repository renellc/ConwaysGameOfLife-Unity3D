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
        int livingTotal = LivingCellSides(grid) + LivingCellAdjacent(grid);
        return livingTotal;
    }

    /// <summary>
    /// Gets the number of living cells on the sides of the cell.
    /// </summary>
    /// <param name="grid">The current state of the grid.</param>
    /// <returns>The number of living cells on the sides of the cell.</returns>
    private int LivingCellSides(Cell[,] grid)
    {
        int sideCount = 0;

        // Cell to the right
        if (IsValidCoordinate(grid, X + 1, Y) && grid[X + 1, Y].Alive)
        {
            sideCount++;
        }

        // Cell to the left
        if (IsValidCoordinate(grid, X - 1, Y) && grid[X - 1, Y].Alive)
        {
            sideCount++;
        }

        // Cell to the top
        if (IsValidCoordinate(grid, X, Y + 1) && grid[X, Y + 1].Alive)
        {
            sideCount++;
        }

        // Cell to the bottom
        if (IsValidCoordinate(grid, X, Y - 1) && grid[X, Y - 1].Alive)
        {
            sideCount++;
        }

        return sideCount;
    }

    /// <summary>
    /// Gets the number of living cells adjacent to this cell.
    /// </summary>
    /// <param name="grid">The current state of the grid.</param>
    /// <returns>The number of living cells adjacent to this cell.</returns>
    private int LivingCellAdjacent(Cell[,] grid)
    {
        int adjCount = 0;

        // Cell to the top right
        if (IsValidCoordinate(grid, X + 1, Y + 1) && grid[X + 1, Y + 1].Alive)
        {
            adjCount++;
        }

        // Cell to the bottom right
        if (IsValidCoordinate(grid, X + 1, Y - 1) && grid[X + 1, Y - 1].Alive)
        {
            adjCount++;
        }

        // Cell to the top left
        if (IsValidCoordinate(grid, X - 1, Y + 1) && grid[X + 1, Y + 1].Alive)
        {
            adjCount++;
        }

        // Cell to the bottom left
        if (IsValidCoordinate(grid, X - 1, Y - 1) && grid[X + 1, Y + 1].Alive)
        {
            adjCount++;
        }

        return adjCount;
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
