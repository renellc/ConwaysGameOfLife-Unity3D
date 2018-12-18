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

    private void Start()
    {
        game = GetComponent<GameOfLife>();
    }

    private void Update()
    {
        // Sets a tile as alive on the grid.
        if (Input.GetMouseButton(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            var currentCellPos = tilemap.WorldToCell(mousePos);

            if (!game.gridState[currentCellPos.x, currentCellPos.y].Alive)
            {
                game.gridState[currentCellPos.x, currentCellPos.y].Alive = true;
                tilemap.SetTile(currentCellPos, game.aliveTile);
            }
        }

        if (Input.GetMouseButton(1))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            var currentCellPos = tilemap.WorldToCell(mousePos);

            if (game.gridState[currentCellPos.x, currentCellPos.y].Alive)
            {
                game.gridState[currentCellPos.x, currentCellPos.y].Alive = false;
                tilemap.SetTile(currentCellPos, game.deadTile);
            }
        }
    }
}
