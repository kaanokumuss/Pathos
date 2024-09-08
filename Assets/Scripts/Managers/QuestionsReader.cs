using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsReader : MonoBehaviour
{
    [SerializeField] private QuestionDataSO questionData; 
    [SerializeField] private TextAsset jsonTextAsset; 

    void Start()
    {
        LoadQuestionsFromJSON();
    }

    void LoadQuestionsFromJSON()
    {
        if (jsonTextAsset == null)
        {
            Debug.LogError("JSON TextAsset is not assigned.");
            return;
        }

        string json = jsonTextAsset.text;
        QuestionDataWrapper wrapper = JsonUtility.FromJson<QuestionDataWrapper>(json);

        if (wrapper != null && questionData != null)
        {
            questionData.questions.Clear();
            questionData.questions.AddRange(wrapper.questions);

        }

    }
}
