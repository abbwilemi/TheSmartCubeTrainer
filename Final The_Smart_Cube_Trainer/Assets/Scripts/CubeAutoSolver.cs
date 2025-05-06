using UnityEngine;
using System.Collections.Generic;
using Kociemba;

public class CubeAutoSolver : MonoBehaviour
{
    public CubeRayCaster cubeReader;
    public CubeState cubeState;

    private bool hasSolvedOnce = true;

    void Start()
    {
        cubeReader = FindObjectOfType<CubeRayCaster>();
        cubeState = FindObjectOfType<CubeState>();
    }

    void Update()
    {
        if (CubeState.started && hasSolvedOnce)
        {
            hasSolvedOnce = false;
            SolveCube();
        }
    }

    /// <summary>
    /// Reads cube state and attempts to solve it using the Kociemba algorithm.
    /// </summary>
    public void SolveCube()
    {
        cubeReader.ReadState();

        string currentState = cubeState.GetStateString();
        Debug.Log("Cube state: " + currentState);

        string debugInfo = "";
        string solution = Search.solution(currentState, out debugInfo);

        List<string> moves = ParseSolutionString(solution);
        CubeAutomator.moveQueue = moves;

        Debug.Log("Solver Info: " + debugInfo);
    }

    /// <summary>
    /// Converts the solution string into a list of move instructions.
    /// </summary>
    List<string> ParseSolutionString(string solution)
    {
        return new List<string>(
            solution.Split(new[] { " " }, System.StringSplitOptions.RemoveEmptyEntries)
        );
    }
}
