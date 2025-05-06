using UnityEngine;
using System.Collections.Generic;

public class CubeRayCaster : MonoBehaviour
{
    // References to the six face Transforms of the cube
    public Transform tUp, tDown, tLeft, tRight, tFront, tBack;

    // Lists of GameObjects that represent ray starting points for each face
    private List<GameObject> raysFront = new();
    private List<GameObject> raysBack = new();
    private List<GameObject> raysUp = new();
    private List<GameObject> raysDown = new();
    private List<GameObject> raysLeft = new();
    private List<GameObject> raysRight = new();

    // Bitmask for detecting only the cube layer (layer 6)
    private int cubeLayer = 1 << 6;

    // References to other components controlling cube logic and visualization
    private CubeState cubeState;
    private CubeVisualizer cubeVisualizer;

    // Prefab used for instantiating ray start points
    public GameObject rayOriginPrefab;

    // Called once when the script starts
    void Start()
    {
        // Set up the ray origins for all six sides
        InitializeRaycasts();

        // Find the CubeState and CubeVisualizer components in the scene
        cubeState = FindObjectOfType<CubeState>();
        cubeVisualizer = FindObjectOfType<CubeVisualizer>();

        // Read the initial cube state
        ReadState();

        // Mark the cube as initialized
        CubeState.started = true;
    }

    // Reads the current state of the cube using raycasts and updates the state and visualizer
    public void ReadState()
    {
        cubeState = FindObjectOfType<CubeState>();
        cubeVisualizer = FindObjectOfType<CubeVisualizer>();

        // Read and assign the state of each cube face
        cubeState.up = CaptureFace(raysUp, tUp);
        cubeState.down = CaptureFace(raysDown, tDown);
        cubeState.left = CaptureFace(raysLeft, tLeft);
        cubeState.right = CaptureFace(raysRight, tRight);
        cubeState.front = CaptureFace(raysFront, tFront);
        cubeState.back = CaptureFace(raysBack, tBack);

        // Update the visual representation of the cube
        cubeVisualizer.Set();
    }

    // Creates ray origins for all six faces with correct rotations
    void InitializeRaycasts()
    {
        raysUp = CreateFaceRays(tUp, new Vector3(90, 90, 0));
        raysDown = CreateFaceRays(tDown, new Vector3(270, 90, 0));
        raysLeft = CreateFaceRays(tLeft, new Vector3(0, 180, 0));
        raysRight = CreateFaceRays(tRight, Vector3.zero);
        raysFront = CreateFaceRays(tFront, new Vector3(0, 90, 0));
        raysBack = CreateFaceRays(tBack, new Vector3(0, 270, 0));
    }

    // Creates a 3x3 grid of ray origins on the specified face
    List<GameObject> CreateFaceRays(Transform face, Vector3 rotation)
    {
        var rayList = new List<GameObject>();
        int index = 0;

        for (int y = 1; y >= -1; y--) // Top to bottom
        {
            for (int x = -1; x <= 1; x++) // Left to right
            {
                Vector3 localOffset = new(x, y, 0); // Offset from center
                Vector3 worldPos = face.localPosition + localOffset;

                // Instantiate the ray origin point and parent it to the face
                GameObject originPoint = Instantiate(rayOriginPrefab, worldPos, Quaternion.identity, face);
                originPoint.name = index.ToString(); // Useful for debugging
                rayList.Add(originPoint);

                index++;
            }
        }

        // Rotate the face so raycasts shoot in the correct direction
        face.localRotation = Quaternion.Euler(rotation);
        return rayList;
    }

    // Shoots raycasts from each origin on a face and collects the hit GameObjects
    public List<GameObject> CaptureFace(List<GameObject> rayOrigins, Transform faceTransform)
    {
        var detectedFaces = new List<GameObject>();

        foreach (var originObj in rayOrigins)
        {
            Vector3 rayOrigin = originObj.transform.position;
            RaycastHit hit;

            // Cast a ray forward from each origin point
            if (Physics.Raycast(rayOrigin, faceTransform.forward, out hit, Mathf.Infinity, cubeLayer))
            {
                Debug.DrawRay(rayOrigin, faceTransform.forward * hit.distance, Color.yellow);
                detectedFaces.Add(hit.collider.gameObject); // Add the hit cube piece
            }
            else
            {
                // Draw a long green ray if nothing is hit
                Debug.DrawRay(rayOrigin, faceTransform.forward * 1000, Color.green);
            }
        }

        return detectedFaces;
    }
}
