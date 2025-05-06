using UnityEngine;
using System.Collections.Generic;

public class AxisRotation : MonoBehaviour
{
    private List<GameObject> activeSide;  // The currently active side that is being rotated
    private Vector3 localForward;  // Direction for the local axis rotation (used for auto-rotation)
    private Vector3 mouseRef;  // Stores the previous mouse position for calculating movement
    private bool dragging = false;  // Whether the user is dragging the cube
    private bool autoRotating = false;  // Flag for automatic rotation
    private float speed = 300f;  // Speed of the auto-rotation

    private Quaternion targetQuaternion;  // Target rotation quaternion for auto-rotation

    private CubeRayCaster readCube;  // Reference to the ReadCube script
    private CubeState cubeState;  // Reference to the CubeState script

    private static readonly Vector3 sensitivityVector = new Vector3(0.4f, 0.4f, 0.4f); // Unified sensitivity for rotation

    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<CubeRayCaster>();  // Find the ReadCube script in the scene
        cubeState = FindObjectOfType<CubeState>();  // Find the CubeState script in the scene
    }

    // LateUpdate is called after all Update calls
    void LateUpdate()
    {
        // Handle user dragging if not auto-rotating
        if (dragging && !autoRotating)
        {
            SpinSide(activeSide);  // Rotate the active side based on mouse input
            if (Input.GetMouseButtonUp(0))  // When the mouse button is released
            {
                dragging = false;  // Stop dragging
                RotateToRightAngle();  // Snap to the closest right angle
            }
        }

        // Handle automatic rotation
        if (autoRotating)
        {
            AutoRotate();  // Perform auto-rotation toward the target quaternion
        }
    }

    // Spins the currently active side based on mouse movement
    private void SpinSide(List<GameObject> side)
    {
        Vector3 mouseOffset = (Input.mousePosition - mouseRef);  // Calculate the mouse movement offset
        Vector3 rotation = GetRotationForSide(side, mouseOffset);  // Get the rotation for the side
        transform.Rotate(rotation, Space.Self);  // Rotate the cube around its local axis
        mouseRef = Input.mousePosition;  // Update the reference position for the next frame
    }

    // Dynamically calculates the rotation vector based on the side being rotated
    private Vector3 GetRotationForSide(List<GameObject> side, Vector3 mouseOffset)
    {
        float multiplier = 0f;  // Rotation multiplier based on side orientation

        // Set multiplier based on the side being rotated
        if (side == cubeState.front) multiplier = -1f;
        else if (side == cubeState.back) multiplier = 1f;
        else if (side == cubeState.up) multiplier = 1f;
        else if (side == cubeState.down) multiplier = -1f;
        else if (side == cubeState.left) multiplier = 1f;
        else if (side == cubeState.right) multiplier = -1f;

        // Calculate rotation along the correct axis
        Vector3 rotation = Vector3.zero;
        if (side == cubeState.front || side == cubeState.back)
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivityVector.x * multiplier;
        else if (side == cubeState.up || side == cubeState.down)
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivityVector.y * multiplier;
        else if (side == cubeState.left || side == cubeState.right)
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivityVector.z * multiplier;

        return rotation;  // Return the calculated rotation vector
    }

    // Begins rotation on the selected side by tracking the mouse input
    public void Rotate(List<GameObject> side)
    {
        activeSide = side;  // Set the active side to the selected one
        mouseRef = Input.mousePosition;  // Capture the initial mouse position
        dragging = true;  // Start the dragging process
        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;  // Local forward direction
    }

    // Starts auto-rotation toward a specific angle
    public void StartAutoRotate(List<GameObject> side, float angle)
    {
        cubeState.PickUp(side);  // Pick up the side to be rotated
        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;  // Get the local forward direction
        targetQuaternion = Quaternion.AngleAxis(angle, localForward) * transform.localRotation;  // Set the target rotation
        activeSide = side;  // Set the active side
        autoRotating = true;  // Enable auto-rotation
    }

    // Snaps the cube's rotation to the nearest right angle (multiples of 90)
    public void RotateToRightAngle()
    {
        Vector3 roundedAngles = new Vector3(
            Mathf.Round(transform.localEulerAngles.x / 90) * 90,
            Mathf.Round(transform.localEulerAngles.y / 90) * 90,
            Mathf.Round(transform.localEulerAngles.z / 90) * 90
        );

        targetQuaternion.eulerAngles = roundedAngles;  // Set the target quaternion for auto-rotation
        autoRotating = true;  // Enable auto-rotation
    }

    // Handles automatic rotation toward the target quaternion
    private void AutoRotate()
    {
        float step = speed * Time.deltaTime;  // Calculate the rotation speed step
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);  // Rotate toward target

        // If the rotation is close enough to the target quaternion, stop auto-rotation
        if (Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1f)
        {
            transform.localRotation = targetQuaternion;  // Snap to target rotation
            cubeState.PutDown(activeSide, transform.parent);  // Update cube state
            readCube.ReadState();  // Re-read the state after rotation
            CubeState.autoRotating = false;  // Disable auto-rotation flag

            autoRotating = false;  // Stop auto-rotation
            dragging = false;  // Stop dragging
        }
    }
}
