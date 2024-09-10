using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailManager : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private GameObject failPanel;
    [SerializeField] private Sphere sphere; 
    [SerializeField] private GameObject background;
    [SerializeField] private AudioSource fallDown;
    [SerializeField] private AudioSource clickButton;

    private void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    void FixedUpdate()
    {
        Fail();
    }

    private void Fail()
    {
        if (sphere.transform.position.y < -3)
        {
            fallDown.Play();

            background.SetActive(false);
            failPanel.SetActive(true);
            Time.timeScale = 0f; 
        }
    }

    private void RestartGame()
    {
        clickButton.Play();
        Time.timeScale = 1f; 
        SceneEvents.OnLoadGameScene?.Invoke();
    }

    private void LoadMainMenu()
    {
        clickButton.Play();
        Time.timeScale = 1f; 
        SceneEvents.OnLoadMetaScene?.Invoke();
        
    }
}