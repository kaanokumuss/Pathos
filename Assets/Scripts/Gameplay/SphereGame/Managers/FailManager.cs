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
    [SerializeField] private Sphere sphere; // Assuming Sphere is your custom class, or you can replace it with GameObject
    [SerializeField] private GameObject background;

    private void Start()
    {
        // Assigning the button click events
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    void FixedUpdate()
    {
        Fail();
    }

    private void Fail()
    {
        // Check if the sphere has fallen below the threshold
        if (sphere.transform.position.y < -3)
        {
            background.SetActive(false);
            failPanel.SetActive(true);
            Time.timeScale = 0f; // Pauses the game
        }
    }

    // Method to restart the game
    private void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game time before reloading
        SceneEvents.OnLoadGameScene?.Invoke();
    }

    // Method to load the main menu
    private void LoadMainMenu()
    {
        
        Time.timeScale = 1f; // Resume the game time before switching scenes
        SceneEvents.OnLoadMetaScene?.Invoke();
        
    }
}