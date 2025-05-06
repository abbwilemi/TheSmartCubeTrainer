using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CubeVisualizer : MonoBehaviour
{
    // Cached reference to CubeState
    private CubeState cubeState;

    // References to the faces of the cube (up, down, front, back, left, right)
    public Transform up;
    public Transform down;
    public Transform front;
    public Transform back;
    public Transform left;
    public Transform right;

    // A dictionary to map each face letter to its corresponding color
    private readonly Dictionary<char, Color> faceColorMap = new Dictionary<char, Color>
    {
        {'F', new Color(1f, 0.5f, 0f, 1f)}, // Orange
        {'B', Color.red},                  // Red
        {'U', Color.yellow},               // Yellow
        {'D', Color.white},                // White
        {'L', Color.green},                // Green
        {'R', Color.blue}                  // Blue
    };

    // Start is called before the first frame update
    void Start()
    {
        // Cache the CubeState component at the start to avoid repeatedly calling FindObjectOfType
        cubeState = FindObjectOfType<CubeState>();
    }

    // Set the colors of all faces of the cube
    public void Set()
    {
        UpdateFaceColors(cubeState.front, front);
        UpdateFaceColors(cubeState.back, back);
        UpdateFaceColors(cubeState.left, left);
        UpdateFaceColors(cubeState.right, right);
        UpdateFaceColors(cubeState.up, up);
        UpdateFaceColors(cubeState.down, down);
    }

    // Update the color of the side based on the face's GameObject names
    private void UpdateFaceColors(List<GameObject> face, Transform side)
    {
        int count = Mathf.Min(face.Count, side.childCount);  // Safely handle mismatch between face count and side child count

        for (int i = 0; i < count; i++)
        {
            char faceLetter = GetFaceLetter(face[i]);  // Extract the face letter (first character of the GameObject name)
            Image image = side.GetChild(i).GetComponent<Image>();  // Get the Image component of the current child

            // Set the color based on the face letter, default to black if invalid
            image.color = GetColorForFace(faceLetter);
        }
    }

    // Extracts the face letter (first character of the GameObject name)
    private char GetFaceLetter(GameObject face)
    {
        return face.name.Length > 0 ? face.name[0] : 'X';  // Returns 'X' if name is empty
    }

    // Returns the corresponding color for a given face letter
    private Color GetColorForFace(char faceLetter)
    {
        // Return color if the face letter exists in the dictionary, otherwise fallback to black
        return faceColorMap.TryGetValue(faceLetter, out Color color) ? color : Color.black;
    }
}
