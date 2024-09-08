using System;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;

public class Sphere : MonoBehaviour
{
    [SerializeField] private GameObject pressSpace;    
    [SerializeField] private PathGenerator pathGenerator;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] private float destroyCubeSeconds = 0;
    [SerializeField] private float delayBeforeDestroy = 1f;
    private float turnAngle90 = 90f;
    private float turnAngle270 = 270f;
    private Vector3 moveDirection = Vector3.forward;
    private bool turnAngleIs90 = true;

    private CancellationTokenSource cancellationTokenSource;

    private bool isGameStarted = false; 

    void Update()
    {
        if (!isGameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pressSpace.SetActive(false);
                isGameStarted = true;
            }
        }
        else
        {
            pathGenerator.GenerateDefaultPath();
            SphereMovement();
        }
    }

    

    private void SphereMovement()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float angle = turnAngleIs90 ? turnAngle90 : turnAngle270;
            moveDirection = Quaternion.Euler(0, angle, 0) * moveDirection;
            turnAngleIs90 = !turnAngleIs90;
        }
    }

    private async void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            pathGenerator.GeneratePathOnTouch(collision.transform.position);

            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            GameObject collidedObject = collision.gameObject;

            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delayBeforeDestroy), cancellationToken: token);

                float fallDistance = 0.5f;
                Vector3 initialPosition = collidedObject.transform.position;
                Vector3 targetPosition = initialPosition - new Vector3(0, fallDistance, 0);

                collidedObject.transform.DOMove(targetPosition, destroyCubeSeconds)
                    .SetEase(Ease.InBack)
                    .OnComplete(() =>
                    {
                        collidedObject.transform.DOScale(Vector3.zero, 0.5f)
                            .SetEase(Ease.InBack)
                            .OnComplete(() =>
                            {
                                if (collidedObject != null)
                                {
                                    Destroy(collidedObject);
                                }
                            });
                    });
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Yok etme işlemi iptal edildi!");
            }
        }
    }

   
}
