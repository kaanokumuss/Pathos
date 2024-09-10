using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private AudioSource clickButton;
    [SerializeField] Button button;
    
    void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    

    void OnClick()
    { 
        clickButton.Play();
       SceneEvents.OnLoadGameScene?.Invoke();
    }

#if UNITY_EDITOR
    [Button]
    void FindButton()
    {
        button = GetComponent<Button>();
    }
#endif

}
