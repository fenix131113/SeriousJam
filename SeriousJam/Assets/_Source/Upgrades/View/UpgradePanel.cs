using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Upgrades.States;
using VContainer;
using World;
using World.States;

namespace Upgrades.View
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private RectTransform panel;
        [SerializeField] private float switchTime;

        [Inject] private WorldTransitionStateMachine _stateMachine;

        private Type _previousState;

        private void Start() => Bind();

        private void OnDestroy() => Expose();

        public void SetUpgradeButtonActive(bool active) => upgradeButton.gameObject.SetActive(active);
        public void SetCloseButtonActive(bool active) => closeButton.gameObject.SetActive(active);

        private void OpenUpgradePanel()
        {
            _previousState = _stateMachine.CurrentStateType;
            _stateMachine.Switch<TransitionState>();
            panel.DOAnchorPos(Vector2.zero, switchTime).onComplete +=
                () => _stateMachine.Switch<UpgradeLocationState>();
        }

        private void CloseUpgradePanel()
        {
            _stateMachine.Switch<TransitionState>();
            panel.DOAnchorPos(new Vector2(0, -Screen.height), switchTime).onComplete +=
                () => _stateMachine.Switch(_previousState);
        }

        private void Bind()
        {
            upgradeButton.onClick.AddListener(OpenUpgradePanel);
            closeButton.onClick.AddListener(CloseUpgradePanel);
        }

        private void Expose()
        {
            upgradeButton.onClick.RemoveAllListeners();
            closeButton.onClick.RemoveAllListeners();
        }
    }
}