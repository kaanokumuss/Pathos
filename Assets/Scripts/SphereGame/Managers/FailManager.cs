using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailManager : MonoBehaviour
{   
    [SerializeField] private GameObject failPanel;
    [SerializeField] private Sphere sphere;
    [SerializeField] private GameObject background;

    void FixedUpdate()
    {
        Fail();
    }
    
    private void Fail()
    {
        if (sphere.transform.position.y < -3)
        {
            background.SetActive(false);
            failPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
