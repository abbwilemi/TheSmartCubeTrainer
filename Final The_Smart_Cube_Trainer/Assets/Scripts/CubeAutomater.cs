using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeAutomator : MonoBehaviour
{
    // List of moves to be executed by the cube solver
    public static List<string> moveQueue = new List<string>();

    private CubeState cube;       // Reference to current cube state
    private CubeRayCaster reader;      // Reference to the cube-reading logic

    // All possible moves the cube can perform
    private readonly List<string> possibleMoves = new List<string>
    {
        "U", "D", "L", "R", "F", "B",
        "U2", "D2", "L2", "R2", "F2", "B2",
        "U'", "D'", "L'", "R'", "F'", "B'"
    };

    // Dictionary mapping move strings to the actions that perform them
    private delegate void MoveAction();
    private Dictionary<string, MoveAction> moveActions;

    void Start()
    {
        // Find references to cube state and cube reader components
        cube = FindObjectOfType<CubeState>();
        reader = FindObjectOfType<CubeRayCaster>();

        // Initialize the dictionary with all move actions
        InitializeMoveActions();
    }

    void Update()
    {
        // If there are moves queued and the cube is ready, execute the next move
        if (moveQueue.Count > 0 && !CubeState.autoRotating && CubeState.started)
        {
            ExecuteMove(moveQueue[0]);
            moveQueue.RemoveAt(0); // Remove the move after execution
        }
    }

    // Generates a random scramble sequence and adds it to the move queue
    public void GenerateScramble()
    {
        List<string> sequence = new List<string>();
        int count = Random.Range(10, 30); // Random scramble length

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, possibleMoves.Count);
            sequence.Add(possibleMoves[index]);
        }

        moveQueue = sequence; // Apply generated scramble to move queue
    }

    // Populates the moveActions dictionary with delegates that perform each move
    private void InitializeMoveActions()
    {
        moveActions = new Dictionary<string, MoveAction>
        {
            { "U",  () => Rotate(cube.up,   -90) },
            { "U'", () => Rotate(cube.up,    90) },
            { "U2", () => Rotate(cube.up,  -180) },

            { "D",  () => Rotate(cube.down, -90) },
            { "D'", () => Rotate(cube.down,  90) },
            { "D2", () => Rotate(cube.down, -180) },

            { "L",  () => Rotate(cube.left, -90) },
            { "L'", () => Rotate(cube.left,  90) },
            { "L2", () => Rotate(cube.left, -180) },

            { "R",  () => Rotate(cube.right, -90) },
            { "R'", () => Rotate(cube.right,  90) },
            { "R2", () => Rotate(cube.right, -180) },

            { "F",  () => Rotate(cube.front, -90) },
            { "F'", () => Rotate(cube.front,  90) },
            { "F2", () => Rotate(cube.front, -180) },

            { "B",  () => Rotate(cube.back, -90) },
            { "B'", () => Rotate(cube.back,  90) },
            { "B2", () => Rotate(cube.back, -180) }
        };
    }

    // Executes a single move from the move queue
    private void ExecuteMove(string move)
    {
        reader.ReadState();              // Read cube's current state
        CubeState.autoRotating = true;  // Prevent other moves during rotation

        if (moveActions.ContainsKey(move))
        {
            moveActions[move]();        // Perform the move
        }
        else
        {
            Debug.LogWarning($"Unrecognized move: {move}");
        }
    }

    // Handles actual rotation logic using PivotRotation script
    private void Rotate(List<GameObject> face, float angle)
    {
        AxisRotation pivot = face[4].transform.parent.GetComponent<AxisRotation>();
        pivot.StartAutoRotate(face, angle);
    }
}
