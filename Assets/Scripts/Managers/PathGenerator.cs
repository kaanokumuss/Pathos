using System;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Transform pathParent; // Parent transform

    private float cubeSpacing = 1f;
    private int defaultCubes = 70;

    private Vector3 currentPosition;
    private Vector3 currentDirection = Vector3.forward;
    private Transform lastCube;
    private bool isFirstTurn = true;
    private int cubeCount = 0;

    private void Awake()
    {
        GenerateDefaultPath();
    }

    private void GenerateDefaultPath()
    {
        lastCube = Instantiate(cubePrefab, currentPosition, Quaternion.identity, pathParent).transform;
        cubeCount++;

        for (int i = 0; i < defaultCubes; i++)
        {
            currentPosition = lastCube.position + currentDirection * cubeSpacing;

            lastCube = Instantiate(cubePrefab, currentPosition, Quaternion.identity, pathParent).transform;
            cubeCount++;
            HandleTurns();
        }
    }

    public void GeneratePathOnTouch(Vector3 touchPosition)
    {
        currentPosition = lastCube.position + currentDirection * cubeSpacing;

        lastCube = Instantiate(cubePrefab, currentPosition, Quaternion.identity, pathParent).transform;
        cubeCount++;

        HandleTurns();
    }

    void HandleTurns()
    {
        if (cubeCount % UnityHelpers.GetRandomIntInRange(3, 6) == 0)
        {
            if (isFirstTurn)
            {
                Turn90Degrees();
                isFirstTurn = false;
            }
            else
            {
                Turn270Degrees();
                isFirstTurn = true;
            }
        }
    }

    private void Turn270Degrees()
    {
        currentDirection = Quaternion.Euler(0, 270, 0) * currentDirection;
    }

    private void Turn90Degrees()
    {
        currentDirection = Quaternion.Euler(0, 90, 0) * currentDirection;
    }
}