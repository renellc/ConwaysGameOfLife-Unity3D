using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(GameOfLife))]
public class MouseControl : MonoBehaviour
{
    /// <summary>
    /// The tilemap that contains the grid.
    /// </summary>
    [SerializeField]
    private Tilemap tilemap;

    /// <summary>
    /// The reference to the GameOfLife component.
    /// </summary>
    private GameOfLife game;

    /// <summary>
    /// Is the user allowed to edit the board? 
    /// </summary>
    [HideInInspector]
    public bool allowedToEdit;

    private void Start()
    {
        game = GetComponent<GameOfLife>();
        allowedToEdit = true;
    }

    private void Update()
    {
        if (!allowedToEdit)
        {
            return;
        }

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        var cell = tilemap.WorldToCell(mousePos);

        // Sets a tile as alive on the board.
        if (!game.isRunning && Input.GetMouseButton(0) && !game.gridState[cell.x, cell.y].Alive)
        {
            game.gridState[cell.x, cell.y].Alive = true;
            tilemap.SetTile(cell, game.aliveTile);
        }

        // Sets a tile as dead on the board.
        if (!game.isRunning && Input.GetMouseButton(1) && game.gridState[cell.x, cell.y].Alive)
        {
            game.gridState[cell.x, cell.y].Alive = false;
            tilemap.SetTile(cell, game.deadTile);
        }
    }

    public void OnUIMouseHoverEnter()
    {
        allowedToEdit = false;
    }

    public void OnUIMouseHoverExit()
    {
        allowedToEdit = true;
    }
}
