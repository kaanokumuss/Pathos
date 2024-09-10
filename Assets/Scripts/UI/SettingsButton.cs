using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton: MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    public Button quitButton;

    void OnEnable()
    {
        quitButton.onClick.AddListener(OnClick);
    }

    void OnDisable()
    {
        quitButton.onClick.RemoveListener(OnClick);
    }
    

    void OnClick()
    {
        settingsPanel.SetActive(true);
    }
}