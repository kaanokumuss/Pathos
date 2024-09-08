using System;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Transform pathParent;

    private float cubeSpacing = 1f;
    private int defaultCubes = 70;

    private Vector3 currentPosition;
    private Vector3 currentDirection = Vector3.forward;
    private Transform lastCube;
    private bool isFirstTurn = true;
    private int cubeCount = 0;

    private void Start()
    {
        GameEvents.CorrectAnswerCounter += GeneratePathByCorrectAnswer;
    }

    private void OnDestroy()
    {
        GameEvents.CorrectAnswerCounter -= GeneratePathByCorrectAnswer;
    }

    public void GenerateDefaultPath()
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

    private void HandleTurns()
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

    public void GeneratePathByCorrectAnswer(int correctAnswer)
    {
        cubeSpacing = correctAnswer;
        switch (correctAnswer)
        {
            case 1:
                cubePrefab.transform.localScale = new Vector3(1, 1, 1);
                break;

            case 2:
                cubePrefab.transform.localScale = new Vector3(2, 1, 2);
                break;

            case 3:
                cubePrefab.transform.localScale = new Vector3(3, 1, 3);
                break;

            case 4:
                cubePrefab.transform.localScale = new Vector3(4, 1, 4);
                break;
            case 5:
                cubePrefab.transform.localScale = new Vector3(5, 1, 5);
                break;

            default:
                Debug.LogError("Invalid correctAnswer value. Must be 1, 2, 3, or 4.");
                break;
        }
    }
}
