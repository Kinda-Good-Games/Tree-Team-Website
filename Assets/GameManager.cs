using System;
using System.Collections;
using System.Collections.Generic;
using TreeQuiz.QuestionManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace TreeQuiz.QuestionManagement
{
    public class GameManager : MonoBehaviour
    {
        // References for other Managers
        private QuestionDataManager questionDataManager;
        private UIManager uiManager;

        private Question currentQuestion;


        public delegate void AnswerEvent(int? answer);
        public event AnswerEvent onReceivedAnswer;
        public int? AnswerIndex
        {
            get { return answerIndex; }
            private set
            {
                answerIndex = value;
                uiManager.ChangeAnswerButtonStates(answerIndex);
            }
        }
        private int? answerIndex;

        public int Points
        {
            get { return points; }
            private set
            {
                points = value;
                uiManager.scoreText.text = points.ToString();
            }
        }
        private int points;
        // Timer
        [SerializeField] private float textWaitTime = 3f;
        [SerializeField] private float maxTime = 10f;
        public float timer { get; private set; }

        // Start is called before the first frame update
        IEnumerator Start()
        {
            onReceivedAnswer += ReceivedAnswer;

            questionDataManager = FindObjectOfType<QuestionDataManager>();
            uiManager = FindObjectOfType<UIManager>();

            yield return new WaitForSeconds(.5f);
            StartQuestion();
            Points = 0;
        }
        /// <summary>
        /// Gets a random Question and Loads it into the UI
        /// </summary>
        private void StartQuestion()
        {
            AnswerIndex = null;
            if (questionDataManager.questions.questions.Count == 0)
            {
                uiManager.ActivateWinUI();

                return;
            }

            int i = Random.Range(0, questionDataManager.questions.questions.Count);
            var question = questionDataManager.questions.questions[i];
            currentQuestion = question;
            questionDataManager.questions.questions.RemoveAt(i);

            uiManager.LoadQuestion(currentQuestion);

            ResetTimer();
            StartCoroutine(StartTimer());
        }
        private IEnumerator StartTimer()
        {
            float lastTime = 0;
            while (timer > 0)
            {
                timer -= Time.deltaTime;

                if (Mathf.Floor(lastTime) != Mathf.Floor(timer))
                {
                }

                lastTime = timer;
                uiManager.SetTimer(timer);
                yield return null;
            }

            onReceivedAnswer.Invoke(AnswerIndex);
        }

        public void SubmitAnswer()
        {
            StopAllCoroutines();
            onReceivedAnswer.Invoke(AnswerIndex);
        }
        private void ReceivedAnswer(int? answer)
        {
            if (AnswerIndex == null)
            {
                StartCoroutine(DisplayExtraLoseMessage());
                AudioManager.instance.Play("Wrong");
                return;
            }


            uiManager.SetAnswerText(answer == currentQuestion.RightAnswer);

            if (answer == currentQuestion.RightAnswer)
            {
                StartCoroutine(DisplayExtraMessage());
                AudioManager.instance.Play("Right");
            }
            else
            {
                StartCoroutine(DisplayExtraLoseMessage());
                AudioManager.instance.Play("Wrong");
            }
        }
        /// <summary>
        /// Displays the message that is presented when lost
        /// </summary>
        /// <returns></returns>
        private IEnumerator DisplayExtraLoseMessage()
        {
            yield return new WaitForSeconds(1);

            uiManager.questionText.text = "You Lost. The game will restart soon";

            yield return new WaitForSeconds(textWaitTime);

            ReloadGame();
        }
        public void ReloadGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        private IEnumerator DisplayExtraMessage()
        {
            yield return new WaitForSeconds(1);

            uiManager.questionText.text = currentQuestion.Comment;
            points++;

            yield return new WaitForSeconds(textWaitTime);

            StartQuestion();
        }
        private void ResetTimer()
        {
            timer = maxTime;
        }
        public void SetAnswer(int isAnswer1)
        {
            AnswerIndex = isAnswer1;
        }
    }
}