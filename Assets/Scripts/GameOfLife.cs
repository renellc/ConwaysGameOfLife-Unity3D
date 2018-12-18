using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class GameOfLife : MonoBehaviour
{
    #region Tiles

    /// <summary>
    /// The tile used for when a cell is alive.
    /// </summary>
    [SerializeField]
    private Tile aliveTile;

    /// <summary>
    /// The tile used for when a cell is dead.
    /// </summary>
    [SerializeField]
    private Tile deadTile;

    #endregion

    public int width, height;

    /// <summary>
    /// The speed at which the simulation will run at.
    /// </summary>
    [SerializeField]
    private float simulationSpeed;

    /// <summary>
    /// The tilemap that contains the cells of our grid.
    /// </summary>
    private Tilemap tilemap;

    /// <summary>
    /// Contains the states of each cell in the grid. If a cell (x, y) is true, the cell is
    /// considered to be alive, otherwise it is dead.
    /// </summary>
    private Cell[,] gridState;

    /// <summary>
    /// Is the simulation currently running?
    /// </summary>
    private bool isRunning;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        
        isRunning = false;
        gridState = new Cell[width, height];

        // All cells start off dead
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridState[x, y] = new Cell(x, y, false);
                tilemap.SetTile(new Vector3Int(x, y, 0), deadTile);
            }
        }
    }

    public void StartGameOfLifeSimulation()
    {
        isRunning = true;
        StartCoroutine(StartSimulation());
    }

    public void StopSimulation()
    {
        isRunning = false;
    }

    private IEnumerator StartSimulation()
    {
        while (isRunning)
        {
            Debug.Log("running sim");
            yield return new WaitForSeconds(simulationSpeed);
        }
    }

    /// <summary>
    /// Updates a cell's state in the grid.
    /// </summary>
    /// <param name="cell">The cell to update.</param>
    /// <returns>The updated state of the cell.</returns>
    private Cell UpdateCellState(Cell cell)
    {
        Cell newCellState = cell;
        int livingCellCount = cell.LivingCellCount(gridState);

        if (cell.Alive)
        {
            newCellState.Alive = livingCellCount > 1 && livingCellCount < 4;
        }
        else
        {
            newCellState.Alive = livingCellCount == 3;
        }

        return newCellState;
    }
}
