using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[RequireComponent(typeof(Tilemap), typeof(MouseControl))]
public class GameOfLife : MonoBehaviour
{
    #region Tiles

    [Header("Board Tiles")]
    /// <summary>
    /// The tile used for when a cell is alive.
    /// </summary>
    public Tile aliveTile;

    /// <summary>
    /// The tile used for when a cell is dead.
    /// </summary>
    public Tile deadTile;

    #endregion

    #region Board settings

    [Header("Board Settings")]
    /// <summary>
    /// The width of the board.
    /// </summary>
    public int width = 48;

    /// <summary>
    /// The height of the board.
    /// </summary>
    public int height = 27;

    /// <summary>
    /// The default speed at which the simulation will run at.
    /// </summary>
    [SerializeField, Range(0, 0.9f)]
    private float defaultSimulationSpeed = 0;

    #endregion

    #region Components

    [Header("Components")]
    /// <summary>
    /// The tilemap that contains the cells of our grid.
    /// </summary>
    [HideInInspector]
    public Tilemap tilemap;

    /// <summary>
    /// The component for the controlling the mouse.
    /// </summary>
    private MouseControl mouseControl;

    /// <summary>
    /// The UI slider component for adjusting the simulation speed.
    /// </summary>
    [SerializeField]
    private Slider simSpeedSlider;

    #endregion

    #region Class members

    /// <summary>
    /// The current simulation speed.
    /// </summary>
    private float simulationSpeed;

    /// <summary>
    /// Contains the states of each cell in the grid. If a cell (x, y) is true, the cell is
    /// considered to be alive, otherwise it is dead.
    /// </summary>
    [HideInInspector]
    public Cell[,] gridState;

    /// <summary>
    /// Is the simulation currently running?
    /// </summary>
    [HideInInspector]
    public bool isRunning;

    #endregion

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        mouseControl = GetComponent<MouseControl>();

        simulationSpeed = defaultSimulationSpeed;
        simSpeedSlider.value = 1 - simulationSpeed;
        
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

        tilemap.ResizeBounds();
    }

    /// <summary>
    /// Starts the game of life simulation.
    /// </summary>
    public void StartGameOfLifeSimulation()
    {
        if (!isRunning)
        {
            isRunning = true;
            mouseControl.allowedToEdit = false;
            StartCoroutine(StartSimulation());
        }
    }

    /// <summary>
    /// Stops the Game of Life simulation.
    /// </summary>
    public void StopSimulation()
    {
        isRunning = false;
        mouseControl.allowedToEdit = true;
    }

    /// <summary>
    /// Starts the Game Of Life simulation.
    /// </summary>
    private IEnumerator StartSimulation()
    {
        while (isRunning)
        {
            // Create a new grid state.
            Cell[,] nextGridState = new Cell[width, height];

            // Update each cell's state and place them in a new grid state.
            for (int x = 0; x < gridState.GetLength(0); x++)
            {
                for (int y = 0; y < gridState.GetLength(1); y++)
                {
                    nextGridState[x, y] = UpdateCellState(gridState, gridState[x, y]);
                }
            }

            // Update our current grid state.
            gridState = nextGridState;

            // Render the new grid state.
            for (int x = 0; x < gridState.GetLength(0); x++)
            {
                for (int y = 0; y < gridState.GetLength(1); y++)
                {
                    if (gridState[x, y].Alive)
                    {
                        tilemap.SetTile(new Vector3Int(x, y, 0), aliveTile);
                    }
                    else
                    {
                        tilemap.SetTile(new Vector3Int(x, y, 0), deadTile);
                    }
                }
            }

            // Wait a set amount of seconds before running this coroutine again.
            yield return new WaitForSeconds(simulationSpeed);
        }
    }

    /// <summary>
    /// Updates a cell's state in the grid.
    /// </summary>
    /// <param name="grid">The current state of the grid.</param>
    /// <param name="cell">The cell to update.</param>
    /// <returns>The updated state of the cell.</returns>
    private Cell UpdateCellState(Cell[,] grid, Cell cell)
    {
        Cell newCellState = new Cell(cell.X, cell.Y, false);
        int livingCellCount = cell.LivingNeighborCount(grid);

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

    public void AdjustSimulationSpeed()
    {
        simulationSpeed = 1 - simSpeedSlider.value;
    }
}
