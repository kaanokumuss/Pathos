using UnityEngine;
using TMPro;

public class CorrectAnswerCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI progressText;
 
    private void Start()
    {
        if (progressText == null)
        {
            Debug.LogError("Progress Text is not assigned.");
        }
    }

    public void UpdateProgress(int correctAnswersCount, int totalQuestions)
    {
        if (progressText != null)
        {
            progressText.text = $"{correctAnswersCount} / {totalQuestions}";
        }
    }
}