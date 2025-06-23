using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGame.View
{
    public class QuestionsStartButton : MonoBehaviour
    {
        [SerializeField] private float moveTime = 0.2f;
        
        private bool _isOpen;
        private float _startX;
        private Button _button;
        private Tween _moveTween;
        private RectTransform _rect;

        public event Action OnClick;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _rect = GetComponent<RectTransform>();
            _startX = _rect.anchoredPosition.x;
        }

        private void Start() => Bind();

        private void OnDestroy() => Expose();

        private void OnButtonClicked()
        {
            if(!_isOpen)
                return;
            
            OnClick?.Invoke();
            HideButton();
        }

        public void ShowButton()
        {
            if (_isOpen)
                return;
            
            _isOpen = true;
            _moveTween = _rect.DOLocalMoveX(_startX + _rect.sizeDelta.x, moveTime);
        }

        public void HideButton()
        {
            if (!_isOpen)
                return;

            _moveTween?.Kill();
            _moveTween = _rect.DOLocalMoveX(_startX, moveTime);
            _isOpen = false;
        }

        private void Bind() => _button.onClick.AddListener(OnButtonClicked);

        private void Expose() => _button.onClick.RemoveAllListeners();
    }
}