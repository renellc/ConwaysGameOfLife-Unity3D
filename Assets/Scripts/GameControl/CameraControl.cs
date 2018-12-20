using ConwaysGameOfLife;
using UnityEngine;

namespace GameControl
{
    [RequireComponent(typeof(Camera))]
    public class CameraControl : MonoBehaviour
    {
        /// <summary>
        /// The main camera for the game.
        /// </summary>
        private Camera viewCamera;

        /// <summary>
        /// The zoom level that indicates the closest we can zoom in.
        /// </summary>
        [SerializeField]
        private int minimumZoomLevel;

        /// <summary>
        /// The zoom level that indicates the farthest we can zoom out.
        /// </summary>
        [SerializeField]
        private int maximumZoomLevel;

        /// <summary>
        /// How fast the camera moves when being panned.
        /// </summary>
        [SerializeField]
        private float panSpeed;

        /// <summary>
        /// The reference to the board.
        /// </summary>
        [SerializeField]
        private GameOfLife board;

        private void Start()
        {
            viewCamera = Camera.main;
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
        /// Moves the camera in some direction (based on user input).
        /// </summary>
        private void MoveCamera()
        {
            // Get the user's input.
            var moveDirection = new Vector3
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = Input.GetAxisRaw("Vertical")
            };

            // We normalize the user input to get the true direction (where the magnitude of the vector
            // is equal to 1).
            moveDirection.Normalize();

            // Scale the move direction by the pan speed.
            moveDirection *= panSpeed * Time.deltaTime;

            // Update the camera's position.
            var targetPos = viewCamera.transform.position + moveDirection;
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
}
