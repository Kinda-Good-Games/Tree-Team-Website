using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;
using KindaGoodUtility;
using System;
using System.IO;
namespace TreeQuiz.QuestionManagement
{
    public class QuestionDataManager : MonoBehaviour
    {
        #region Remote_Config_Stuff
        private struct userAttributes { }
        private struct appAttributes { }
        #endregion

        [field: SerializeField] public QuestionList questions { get; private set; }
        private string questionFileName = "Questions.json";

        private void Awake()
        {
            Debug.Log(JsonUtility.ToJson(questions));
            // Creates the directory and files responsible for question data management if needed
            if (PlayerPrefs.GetInt("QuestionSetupAlreadyDone") == 0)
            {
                if (!Directory.Exists(JsonHelp.DefaultDatapath))
                {
                    JsonHelp.CreateDirectory(JsonHelp.DefaultDatapath);
                }

                questions.SaveJsonFile(JsonHelp.DefaultDatapath, questionFileName);

                PlayerPrefs.SetInt("QuestionSetupAlreadyDone", 1);
            }


        }
        private void OnEnable()
        {
            // "subscribes to the Remote Config"
            ConfigManager.FetchCompleted += OnFetchCompleted;
            ConfigManager.FetchConfigs<userAttributes, appAttributes>(new(), new());
        }
        private void OnDisable()
        {
            Debug.Log("SF");
            // "desubscribes to the Remote Config"
            ConfigManager.FetchCompleted -= OnFetchCompleted;
        }

        /// <summary>
        /// Manages the received data from the remote config
        /// </summary>
        /// <param name="response"></param>
        private void OnFetchCompleted(ConfigResponse response)
        {
            // Reads the data from the remote config and updates the json file
            questions = JsonUtility.FromJson<QuestionList>(ConfigManager.appConfig.GetJson("Questions"));
            questions.SaveJsonFile(JsonHelp.DefaultDatapath, questionFileName);

            FindObjectOfType<UIManager>().maxQuestionCount = questions.questions.Count;
        }
    }
    [Serializable]
    public class QuestionList
    {
        public List<Question> questions;
    }
    [Serializable]
    public class Question
    {
        public const int MAX_QUESTION_COUNT = 4;
        [field: SerializeField]
        public string QuestionText { get; private set; }

        public string[] PossibleAnswers { get; private set; } = new string[MAX_QUESTION_COUNT];
        [field: SerializeField]
        public int RightAnswer { get; private set; }

        [field: SerializeField]

        public string Comment { get; private set; }

        public Question(string questionText, int rightAnswer, params string[] answers)
        {
            QuestionText = questionText;
            RightAnswer = rightAnswer;
            PossibleAnswers = answers;
        }
    }
}