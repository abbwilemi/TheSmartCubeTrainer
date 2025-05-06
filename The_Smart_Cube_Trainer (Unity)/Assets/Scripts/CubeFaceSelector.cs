using System.Collections.Generic;
using UnityEngine;

public class FaceSelector : MonoBehaviour
{
    // Reference to the CubeState script that keeps track of the cube's current state
    CubeState cubeState;

    // Reference to the script responsible for raycasting the cube
    CubeRayCaster readCube;

    // Layer mask to filter raycast hits (only interact with objects on layer 6)
    int layerMask = 1 << 6;

    void Start()
    {
        // Find and store references to the required components in the scene
        readCube = FindObjectOfType<CubeRayCaster>();
        cubeState = FindObjectOfType<CubeState>();
    }

    void Update()
    {
        // Only proceed if the left mouse button is pressed and the cube is not rotating
        if (Input.GetMouseButtonDown(0) && !CubeState.autoRotating)
        {
            // Read the current state of the cube before processing the click
            readCube.ReadState();

            RaycastHit hit;
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Check if the ray hits something within 100 units on the specified layer
            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                // Get the face (GameObject) that was hit by the ray
                GameObject face = hit.collider.gameObject;

                // List of all sides of the cube
                List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.up,
                    cubeState.down,
                    cubeState.left,
                    cubeState.right,
                    cubeState.front,
                    cubeState.back,
                };

                // Loop through each side to check which one contains the clicked face
                foreach (List<GameObject> cubeSide in cubeSides)
                {
                    if (cubeSide.Contains(face))
                    {
                        // Pick up the current state of the matched side
                        cubeState.PickUp(cubeSide);

                        // Initiate rotation of the selected side using its center piece
                        cubeSide[4].transform.parent.GetComponent<AxisRotation>().Rotate(cubeSide);
                    }
                }
            }
        }
    }
}

