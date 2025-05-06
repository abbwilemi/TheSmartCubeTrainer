using UnityEngine;

public class CubeRotater : MonoBehaviour
{
    public GameObject pivot;  // The object that this one will rotate toward and perform swipe rotations on

    Vector2 start, end;        // Stores the start and end positions for swipe gestures
    Vector3 lastMouse;         // Stores the mouse's previous position for drag rotation
    float snapSpeed = 200f;    // Speed at which the object rotates to match the pivot's rotation

    void Update()
    {
        RotateWithMouse();     // Handles rotation when dragging with the mouse
        DetectSwipe();         // Detects swipe gestures to trigger rotations
    }

    void RotateWithMouse()
    {
        if (Input.GetMouseButton(1)) // Right mouse button held down
        {
            // Calculate the movement since the last frame, then apply that movement as rotation
            var delta = (Input.mousePosition - lastMouse) * 0.1f;
            transform.rotation = Quaternion.Euler(delta.y, -delta.x, 0) * transform.rotation;
        }
        else
        {
            // If the mouse button is released, smoothly rotate back to the pivot's rotation
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                pivot.transform.rotation,
                snapSpeed * Time.deltaTime
            );
        }

        // Update the previous mouse position for the next frame
        lastMouse = Input.mousePosition;
    }

    void DetectSwipe()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // Record the position where the right-click started
            start = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            // When the mouse button is released, calculate the swipe direction
            end = Input.mousePosition;
            var swipe = (end - start).normalized;

            // Based on the swipe direction, rotate the pivot object
            if (swipe.x < -0.5f && Mathf.Abs(swipe.y) < 0.5f)
                pivot.transform.Rotate(0, 90, 0, Space.World);         // Swipe left
            else if (swipe.x > 0.5f && Mathf.Abs(swipe.y) < 0.5f)
                pivot.transform.Rotate(0, -90, 0, Space.World);        // Swipe right
            else if (swipe.y > 0.5f && swipe.x < 0)
                pivot.transform.Rotate(90, 0, 0, Space.World);         // Swipe up-left
            else if (swipe.y > 0.5f && swipe.x > 0)
                pivot.transform.Rotate(0, 0, -90, Space.World);        // Swipe up-right
            else if (swipe.y < -0.5f && swipe.x < 0)
                pivot.transform.Rotate(0, 0, 90, Space.World);         // Swipe down-left
            else if (swipe.y < -0.5f && swipe.x > 0)
                pivot.transform.Rotate(-90, 0, 0, Space.World);        // Swipe down-right
        }
    }
}
