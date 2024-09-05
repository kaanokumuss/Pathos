using UnityEngine;

public class Sphere : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    private float turnAngle90 = 90f;
    private float turnAngle270 = 270f;
    private Vector3 moveDirection = Vector3.forward;
    private bool turnAngleIs90 = true; 

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float angle = turnAngleIs90 ? turnAngle90 : turnAngle270;
            moveDirection = Quaternion.Euler(0, angle, 0) * moveDirection;
            turnAngleIs90 = !turnAngleIs90; 
        }
    }
}