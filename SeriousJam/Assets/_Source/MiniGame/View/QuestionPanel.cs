using System;
using DG.Tweening;
using MiniGame.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGame.View
{
    public class QuestionPanel : MonoBehaviour
    {
        [SerializeField] private float moveTime;
        [SerializeField] private TMP_Text descriptionLabel;
        [SerializeField] private TMP_Text answerCounter;
        [SerializeField] private Button firstAnswerButton;
        [SerializeField] private Button secondAnswerButton;
        [SerializeField] private Button thirdAnswerButton;
        [SerializeField] private GameObject upgradeButtonRoot;

        private float _startX;
        private bool _isOpen;
        private RectTransform _rect;
        private Tween _moveTween;
        private QuestionDataSO _currentQuestionGroup;
        private int _currentQuestionIndex;
        private int _rightAnswersCount;

        public event Action<QuestionDataSO, bool> OnQuestCompleted;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _startX = _rect.anchoredPosition.x;
        }

        private void Start() => Bind();

        private void OnDestroy() => Expose();

        public void ShowPanel(QuestionDataSO questionData)
        {
            if (_isOpen)
                return;

            _currentQuestionIndex = 0;
            _rightAnswersCount = 0;
            _currentQuestionGroup = questionData;
            upgradeButtonRoot.SetActive(false);
            UpdateQuestionInfo();

            _moveTween = _rect.DOLocalMoveX(_startX + _rect.sizeDelta.x, moveTime);
            _isOpen = true;
        }

        public void ClosePanel()
        {
            if (!_isOpen)
                return;

            _isOpen = false;
            _moveTween?.Kill();
            _moveTween = _rect.DOLocalMoveX(_startX, moveTime);
            _moveTween.onComplete += () =>
            {
                upgradeButtonRoot.SetActive(true);
                OnQuestCompleted?.Invoke(_currentQuestionGroup, _rightAnswersCount == _currentQuestionGroup.Questions.Count);
            };
        }

        private void OnFirstAnswerClick() => CheckAnswer(0);

        private void OnSecondAnswerClick() => CheckAnswer(1);

        private void OnThirdAnswerClick() => CheckAnswer(2);

        private void CheckAnswer(int answerIndex)
        {
            if (_currentQuestionGroup.Questions[_currentQuestionIndex].AnswerIndex == answerIndex)
                _rightAnswersCount++;

            _currentQuestionIndex++;
            UpdateQuestionInfo();

            if (_currentQuestionIndex < _currentQuestionGroup.Questions.Count)
                return;

            ClosePanel();
        }

        private void UpdateQuestionInfo()
        {
            if (_currentQuestionIndex == _currentQuestionGroup.Questions.Count)
                return;

            answerCounter.text = $"{_currentQuestionIndex + 1}/{_currentQuestionGroup.Questions.Count}";

            descriptionLabel.text = _currentQuestionGroup.Questions[_currentQuestionIndex].Question;
            firstAnswerButton.transform.GetChild(0).GetComponent<TMP_Text>().text =
                _currentQuestionGroup.Questions[_currentQuestionIndex].FirstAnswer;
            secondAnswerButton.transform.GetChild(0).GetComponent<TMP_Text>().text =
                _currentQuestionGroup.Questions[_currentQuestionIndex].SecondAnswer;
            thirdAnswerButton.transform.GetChild(0).GetComponent<TMP_Text>().text =
                _currentQuestionGroup.Questions[_currentQuestionIndex].ThirdAnswer;
        }

        private void Bind()
        {
            firstAnswerButton.onClick.AddListener(OnFirstAnswerClick);
            secondAnswerButton.onClick.AddListener(OnSecondAnswerClick);
            thirdAnswerButton.onClick.AddListener(OnThirdAnswerClick);
        }

        private void Expose()
        {
            firstAnswerButton.onClick.RemoveAllListeners();
            secondAnswerButton.onClick.RemoveAllListeners();
            thirdAnswerButton.onClick.RemoveAllListeners();
        }
    }
}