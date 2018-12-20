using ConwaysGameOfLife;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameControl
{
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
        private GameOfLife board;

        /// <summary>
        /// Is the user allowed to edit the board? 
        /// </summary>
        [HideInInspector]
        public bool allowedToEdit;

        private void Start()
        {
            board = GetComponent<GameOfLife>();
            allowedToEdit = true;
        }

        private void Update()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            var cell = tilemap.WorldToCell(mousePos);

            // Don't do any action if the user happens to press on a tile that doesn't exist or if the
            // the game is currently running.
            if (!allowedToEdit || !IsValidCoordinate(board.gridState, cell.x, cell.y))
            {
                return;
            }

            // Sets a tile as alive on the board.
            if (!board.isRunning && Input.GetMouseButton(0) && !board.gridState[cell.x, cell.y].Alive)
            {
                board.gridState[cell.x, cell.y].Alive = true;
                tilemap.SetTile(cell, board.aliveTile);
            }

            // Sets a tile as dead on the board.
            if (!board.isRunning && Input.GetMouseButton(1) && board.gridState[cell.x, cell.y].Alive)
            {
                board.gridState[cell.x, cell.y].Alive = false;
                tilemap.SetTile(cell, board.deadTile);
            }
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

        public void OnUIMouseHoverEnter()
        {
            allowedToEdit = false;
        }

        public void OnUIMouseHoverExit()
        {
            allowedToEdit = true;
        }
    }
}
