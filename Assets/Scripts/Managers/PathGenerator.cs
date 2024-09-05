using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    private float cubeSpacing = 1f; 
    private float minTurnInterval = 3;
    private float maxTurnInterval = 5;
    private int defaultCubes = 60;

    private Vector3 currentPosition;
    private Vector3 currentDirection = Vector3.forward;
    private Transform lastCube;
    private bool isFirstTurn = true;
    private  int cubeCount= 0;


    private void Start()
    {
        GenerateDefaultPath();
    }

    private void GenerateDefaultPath()
    {
        lastCube = Instantiate(cubePrefab, currentPosition, Quaternion.identity).transform;
        cubeCount++;

        for (int i = 0; i < defaultCubes; i++)
        {
            currentPosition = lastCube.position + currentDirection * cubeSpacing;

            lastCube = Instantiate(cubePrefab, currentPosition, Quaternion.identity).transform;
            cubeCount++;
            HandleTurns();
        }

       
    }

    void HandleTurns()
    {
        if (cubeCount % UnityHelpers.GetRandomIntInRange(3, 5) == 0)
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
