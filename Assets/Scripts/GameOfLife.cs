using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[RequireComponent(typeof(Tilemap), typeof(MouseControl))]
public class GameOfLife : MonoBehaviour
{
    #region Tiles

    /// <summary>
    /// The tile used for when a cell is alive.
    /// </summary>
    public Tile aliveTile;

    /// <summary>
    /// The tile used for when a cell is dead.
    /// </summary>
    public Tile deadTile;

    #endregion

    public int width, height;

    [SerializeField]
    private Slider simSpeedSlider;

    /// <summary>
    /// The dfeault speed at which the simulation will run at.
    /// </summary>
    [SerializeField, Range(0, 0.9f)]
    private float defaultSimulationSpeed;

    private float simulationSpeed;

    /// <summary>
    /// The tilemap that contains the cells of our grid.
    /// </summary>
    public Tilemap tilemap;

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

    private MouseControl mouseControl;

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

    public void StartGameOfLifeSimulation()
    {
        if (!isRunning)
        {
            isRunning = true;
            mouseControl.allowedToEdit = false;
            StartCoroutine(StartSimulation());
        }
    }

    public void StopSimulation()
    {
        isRunning = false;
        mouseControl.allowedToEdit = true;
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

    public void AdjustSimulationSpeed()
    {
        simulationSpeed = 1 - simSpeedSlider.value;
    }
}
