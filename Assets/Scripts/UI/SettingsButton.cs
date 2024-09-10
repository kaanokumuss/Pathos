using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private AudioSource clickButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button settingsButton;

    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private Vector3 targetScale = Vector3.one; 

    void OnEnable()
    {
        quitButton.onClick.AddListener(CloseSettingsPanel);
        settingsButton.onClick.AddListener(OpenSettingsPanel);
    }

    void OnDisable()
    {
        quitButton.onClick.RemoveListener(CloseSettingsPanel);
        settingsButton.onClick.RemoveListener(OpenSettingsPanel);
    }

    void OpenSettingsPanel()
    {
        clickButton.Play();

        if (!settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(true);
            settingsPanel.transform.localScale = Vector3.zero; 
            settingsPanel.transform.DOScale(targetScale, animationDuration).SetEase(Ease.OutBack);
        }
    }

    void CloseSettingsPanel()
    {
        clickButton.Play();

        if (settingsPanel.activeSelf)
        {
            settingsPanel.transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.InBack).OnComplete(() =>
            {
                settingsPanel.SetActive(false); 
            });
        }
    }
}