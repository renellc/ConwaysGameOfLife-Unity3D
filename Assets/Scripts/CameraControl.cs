using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Camera viewCamera;

    [SerializeField]
    private int minimumZoomLevel;

    [SerializeField]
    private int maximumZoomLevel;

    [SerializeField]
    private float panSpeed;

    [SerializeField]
    private GameOfLife board;

    private void Start()
    {
        var tilemapCenter = board.tilemap.cellBounds.center;
        tilemapCenter.z = viewCamera.transform.position.z;
        viewCamera.transform.position = tilemapCenter;
    }

    private void Update()
    {
        MoveCamera();
        ZoomCamera();
    }

    /// <summary>
    /// Moves the camera in soem direction (based on user input).
    /// </summary>
    private void MoveCamera()
    {
        var input = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = Input.GetAxisRaw("Vertical")
        }.normalized;

        var targetPos = viewCamera.transform.position + input * panSpeed * Time.deltaTime;
        viewCamera.transform.position = targetPos;
    }

    /// <summary>
    /// Adjust's the camera's zoom on the board.
    /// </summary>
    private void ZoomCamera()
    {
        // Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            viewCamera.orthographicSize--;
            
        }

        // Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            viewCamera.orthographicSize++;
        }

        // Restrict the camera's zoom by the values set in the inspector.
        viewCamera.orthographicSize = Mathf.Clamp(viewCamera.orthographicSize, minimumZoomLevel,
            maximumZoomLevel);
    }
}
