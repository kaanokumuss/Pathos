using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private GameObject gameplay;
    [SerializeField] private GameObject pressSpaceToStart;
    [SerializeField] private QuestionDataSO questionData;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI questionNumberTextMesh; // Yeni TextMeshPro objesi
    [SerializeField] private QuestionShaker questionShaker;
    [SerializeField] private CorrectAnswerCounter correctAnswerCounter;
    public Button[] optionsButtons;
    [SerializeField] private GameObject questionPanel;
    [SerializeField] private AudioSource audioSource; // Referansını ekle
    [SerializeField] private AudioClip buttonClickSound; // Oynatmak istediğiniz sesi tanımlayın
    private List<int> shuffledIndices;
    public int currentQuestionIndex = 0;

    private int correctAnswersCount = 0;
    private const int totalQuestions = 5;
    private bool isAnswering = false;

    void Start()
    {
        if (questionShaker != null && questionData != null)
        {
            shuffledIndices = questionShaker.ShuffleQuestions(questionData.questions.Count);
            DisplayQuestion();
            gameplay.SetActive(false);
            pressSpaceToStart.SetActive(false);
        }
    }

    void DisplayQuestion()
    {
        if (questionData != null && questionText != null && optionsButtons.Length == 4)
        {
            if (shuffledIndices != null && shuffledIndices.Count > 0)
            {
                int questionIndex = shuffledIndices[currentQuestionIndex];
                QuestionDataSO.Question currentQuestion = questionData.questions[questionIndex];

                string questionNumberText = $"Soru {currentQuestionIndex + 1}:";
                questionNumberTextMesh.text = questionNumberText;

                questionText.text = currentQuestion.question;

                if (correctAnswerCounter != null)
                {
                    correctAnswerCounter.UpdateProgress(GetCorrectAnswersCount(), totalQuestions);
                }

                ResetButtonColors();
                SetButtonsInteractable(true);

                for (int i = 0; i < optionsButtons.Length; i++)
                {
                    int optionIndex = i;
                    optionsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.options[i];
                    optionsButtons[i].onClick.RemoveAllListeners();

                    string correctAnswer = currentQuestion.correctAnswer;
                    optionsButtons[i].onClick.AddListener(() => 
                    { 
                        PlayButtonClickSound(); // Ses oynatma çağrısı
                        CheckAnswer(optionIndex, correctAnswer); 
                    });
                }
            }
            else
            {
                Debug.LogError("No questions available in QuestionDataSO.");
            }
        }
        else
        {
            Debug.LogError("QuestionDataSO, TextMeshProUGUI, or Buttons are not assigned correctly.");
        }
    }

    void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound); // Sesi çal
        }
    }

    async void CheckAnswer(int selectedIndex, string correctAnswer)
    {
        if (isAnswering) return;

        isAnswering = true;
        string selectedOption = optionsButtons[selectedIndex].GetComponentInChildren<TextMeshProUGUI>().text;

        if (selectedOption == correctAnswer)
        {
            Debug.Log("Correct Answer!");
            optionsButtons[selectedIndex].GetComponent<Image>().color = Color.green;
            correctAnswersCount++; 
        }
        else
        {
            Debug.Log("Incorrect Answer!");
            optionsButtons[selectedIndex].GetComponent<Image>().color = Color.red;
        }

        SetButtonsInteractable(false);

        await UniTask.Delay(2000); // Asenkron bekleme
        NextQuestion();
        isAnswering = false;
    }

    void ResetButtonColors()
    {
        foreach (Button button in optionsButtons)
        {
            button.GetComponent<Image>().color = Color.white;
        }
    }

    void SetButtonsInteractable(bool interactable)
    {
        foreach (Button button in optionsButtons)
        {
            button.interactable = interactable;
        }
    }

    public void NextQuestion()
    {
        if (currentQuestionIndex < shuffledIndices.Count - 1)
        {
            currentQuestionIndex++;
            if (currentQuestionIndex < totalQuestions)
            {
                DisplayQuestion();
            }
            else
            {
                pressSpaceToStart.SetActive(true);
                gameplay.SetActive(true);
                EndQuiz();
            }
        }
        else
        {
            EndQuiz();
        }
    }

    void EndQuiz()
    {
        if (questionPanel != null)
        {
            GameEvents.CorrectAnswerCounter?.Invoke(GetCorrectAnswersCount());
            questionPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("QuestionPanel is not assigned.");
        }
    }

    public int GetCorrectAnswersCount()
    {
        return correctAnswersCount;
    }
}
