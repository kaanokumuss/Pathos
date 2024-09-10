using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button settingsButton;

    // Optional: Animation settings
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private Vector3 targetScale = Vector3.one; // End scale (full size)

    void OnEnable()
    {
        // Assign button listeners
        quitButton.onClick.AddListener(CloseSettingsPanel);
        settingsButton.onClick.AddListener(OpenSettingsPanel);
    }

    void OnDisable()
    {
        // Remove button listeners
        quitButton.onClick.RemoveListener(CloseSettingsPanel);
        settingsButton.onClick.RemoveListener(OpenSettingsPanel);
    }

    // Open the settings panel with an animation
    void OpenSettingsPanel()
    {
        if (!settingsPanel.activeSelf)
        {
            // Activate the panel and grow it with animation
            settingsPanel.SetActive(true);
            settingsPanel.transform.localScale = Vector3.zero; // Start from scale 0
            settingsPanel.transform.DOScale(targetScale, animationDuration).SetEase(Ease.OutBack);
        }
    }

    // Close the settings panel with an animation
    void CloseSettingsPanel()
    {
        if (settingsPanel.activeSelf)
        {
            // Animate to shrink and then deactivate
            settingsPanel.transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.InBack).OnComplete(() =>
            {
                settingsPanel.SetActive(false); // Set inactive after animation
            });
        }
    }
}