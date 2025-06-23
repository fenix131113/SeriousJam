using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Upgrades.View
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text descriptionLabel;
        [SerializeField] private float moveTime;

        private float _startX;
        private bool _isOpen;
        private RectTransform _rect;
        private Tween _moveTween;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _startX = _rect.anchoredPosition.x;
        }

        public void ShowPanel(string description)
        {
            descriptionLabel.text = description;

            if (!_isOpen)
                _moveTween = _rect.DOLocalMoveX(_startX + _rect.sizeDelta.x, moveTime);

            _isOpen = true;
        }

        public void ClosePanel()
        {
            if (!_isOpen)
                return;

            _isOpen = false;
            _moveTween.Kill();
            _moveTween = _rect.DOLocalMoveX(_startX, moveTime);
        }
    }
}