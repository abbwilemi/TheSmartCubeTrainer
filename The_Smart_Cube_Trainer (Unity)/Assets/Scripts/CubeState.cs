using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CubeState : MonoBehaviour
{
    // Each list represents the GameObjects on one face of the cube
    public List<GameObject> front = new List<GameObject>();
    public List<GameObject> back = new List<GameObject>();
    public List<GameObject> up = new List<GameObject>();
    public List<GameObject> down = new List<GameObject>();
    public List<GameObject> left = new List<GameObject>();
    public List<GameObject> right = new List<GameObject>();

    // Static flags used globally
    public static bool autoRotating = false;
    public static bool started = false;

    void Start()
    {
        // Placeholder for any startup logic (currently not used)
    }

    void Update()
    {
        // Placeholder for per-frame logic (currently not used)
    }

    // Temporarily re-parents all face pieces to the center piece's parent
    public void PickUp(List<GameObject> cubeSide)
    {
        for (int i = 0; i < cubeSide.Count; i++)
        {
            if (i != 4) // Skip the center piece
            {
                cubeSide[i].transform.parent.transform.parent = cubeSide[4].transform.parent;
            }
        }
    }

    // Re-parents all non-center pieces to a specific pivot (used after rotation)
    public void PutDown(List<GameObject> littleCubes, Transform pivot)
    {
        for (int i = 0; i < littleCubes.Count; i++)
        {
            if (i != 4) // Skip the center piece
            {
                littleCubes[i].transform.parent.transform.parent = pivot;
            }
        }
    }

    // Converts one face of the cube into a string by taking the first char of each GameObject's name
    string GetSideString(List<GameObject> side)
    {
        string result = "";
        foreach (var piece in side)
        {
            result += piece.name[0];
        }
        return result;
    }

    // Returns a complete string representing the cube's current state
    public string GetStateString()
    {
        return GetSideString(up) +
               GetSideString(right) +
               GetSideString(front) +
               GetSideString(down) +
               GetSideString(left) +
               GetSideString(back);
    }
}
