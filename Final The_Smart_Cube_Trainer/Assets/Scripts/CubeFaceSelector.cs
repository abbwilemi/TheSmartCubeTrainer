using System.Collections.Generic;
using UnityEngine;

public class FaceSelector : MonoBehaviour
{
    CubeState cubeState;
    CubeRayCaster readCube;
    int layerMask = 1 << 6;
  

    void Start()
    {
        readCube = FindObjectOfType<CubeRayCaster>();
        cubeState = FindObjectOfType<CubeState>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !CubeState.autoRotating)
        {
            readCube.ReadState();

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                GameObject face = hit.collider.gameObject;

                List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.up,
                    cubeState.down,
                    cubeState.left,
                    cubeState.right,
                    cubeState.front,
                    cubeState.back,
                };
                foreach (List<GameObject> cubeSide in cubeSides)
                {
                    if (cubeSide.Contains(face))
                    {
                        cubeState.PickUp(cubeSide);
                        cubeSide[4].transform.parent.GetComponent<AxisRotation>().Rotate(cubeSide);
                    }
                }

            }
        }
    }
 }

