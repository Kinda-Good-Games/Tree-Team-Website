using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

namespace TreeQuiz.QuestionManagement
{
    public class UIManager : MonoBehaviour
    {
        // References for the UI Elements in the Game Scene
        public TextMeshProUGUI questionText;
        public TextMeshProUGUI scoreText;
        public int maxQuestionCount;
        [SerializeField] private TextMeshProUGUI[] answerTextObjects;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private GameObject winPanel;
        private GameManager gameManager;
        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        public void LoadQuestion(Question question)
        {
            questionText.text = question.QuestionText;
            for (int i = 0; i < answerTextObjects.Length; i++)
            {
                // Sets the button (parent of the text) active based of wether there is an answer available
                if (i < question.PossibleAnswers.Length)
                {
                    answerTextObjects[i].transform.parent.gameObject.SetActive(true);
                    answerTextObjects[i].text = question.PossibleAnswers[i];
                }
                else
                {
                    answerTextObjects[i].transform.parent.gameObject.SetActive(false);
                }
            }

        }
        public void ActivateWinUI()
        {
            winPanel.SetActive(true);
        }
        public void ChangeAnswerButtonStates(int? answer)
        {
            List<Animator> textAnimators = new List<Animator>();
            foreach (var item in answerTextObjects)
            {
                textAnimators.Add(item.GetComponentInParent<Animator>());                       
            }

            // If answer is null, revert the button states. If it is a number set every button except the selected to Hide. The selected button is set to the state Highlight
            if (answer == null)
            {
                textAnimators.ForEach(x => { x?.SetTrigger("Default"); });
            }
            else
            {
                for (int i = 0; i < textAnimators.Count; i++)
                {
                    if (i == answer)
                    {
                        textAnimators[i]?.SetTrigger("Highlight");
                        continue;
                    }

                    textAnimators[i]?.SetTrigger("Hide");
                }
            }
        }
        public void SetTimer(float time)
        {
            timerText.text = (time < 0) ? "" : Mathf.RoundToInt(time).ToString();
        }

        public void SetAnswerText(bool right)
        {
            timerText.text = right ? "RIGHT!" : "Wrong";

            scoreText.text = $"{gameManager.Points} / {maxQuestionCount}";
        }
    }
}