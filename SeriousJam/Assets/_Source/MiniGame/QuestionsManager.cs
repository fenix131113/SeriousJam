using System.Collections.Generic;
using MiniGame.Data;
using MiniGame.View;
using ResourcesSystem;
using UnityEngine;
using VContainer;

namespace MiniGame
{
    public class QuestionsManager
    {
        private readonly QuestionPanel _questionPanel;
        private readonly QuestionsStartButton _questionsStartButton;
        private readonly Wallet _wallet;

        private readonly List<QuestionDataSO> _questions = new();

        [Inject]
        public QuestionsManager(QuestionPanel questionPanel, QuestionsStartButton questionsStartButton, Wallet wallet)
        {
            _questionPanel = questionPanel;
            _questionsStartButton = questionsStartButton;
            _wallet = wallet;
            Bind();
        }

        ~QuestionsManager() => Expose();

        public void AddQuestion(QuestionDataSO question)
        {
            _questions.Add(question);

            if (_questions.Count == 1)
                _questionsStartButton.ShowButton();
        }

        private void ContinueQuestions()
        {
            if (_questions.Count == 0)
                return;

            _questionPanel.ShowPanel(_questions[^1]);
        }

        private void CheckForNewQuestions(QuestionDataSO question, bool correctAnswers)
        {
            if (!_questions.Contains(question))
            {
                Debug.LogWarning("There's no question in list");
                return;
            }

            _questions.Remove(question);

            if (!correctAnswers)
                _questions.Add(question);
            else
                _wallet.AddMoney(question.MoneyForComplete);

            if (_questions.Count > 0)
                _questionsStartButton.ShowButton();
        }

        private void Bind()
        {
            _questionPanel.OnQuestCompleted += CheckForNewQuestions;
            _questionsStartButton.OnClick += ContinueQuestions;
        }

        private void Expose()
        {
            _questionPanel.OnQuestCompleted -= CheckForNewQuestions;
            _questionsStartButton.OnClick -= ContinueQuestions;
        }
    }
}