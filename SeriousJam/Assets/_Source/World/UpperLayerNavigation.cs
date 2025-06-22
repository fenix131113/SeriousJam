using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using World.States;

namespace World
{
    public class UpperLayerNavigation : MonoBehaviour
    {
        [SerializeField] private Transform[] locationsPivots;
        [SerializeField] private Button rightMoveButton;
        [SerializeField] private Button leftMoveButton;
        [SerializeField] private float transitionDuration = 0.5f;

        [Inject] private WorldTransitionStateMachine _stateMachine;
        private int _currentLocationIndex;
        private Camera _camera;

        private void Awake() => _camera = Camera.main;

        private void Start() => Bind();

        private void OnDestroy() => Expose();

        public void CheckButtonActivation()
        {
            leftMoveButton.gameObject.SetActive(true);
            rightMoveButton.gameObject.SetActive(true);

            if (_currentLocationIndex == 0)
                leftMoveButton.gameObject.SetActive(false);
            else if (_currentLocationIndex == locationsPivots.Length - 1)
                rightMoveButton.gameObject.SetActive(false);
        }

        private void OnRightMoveClick()
        {
            _stateMachine.Switch<TransitionState>();
            _currentLocationIndex = _currentLocationIndex + 1 >= locationsPivots.Length ? 0 : _currentLocationIndex + 1;
            _camera.transform.DOMoveX(locationsPivots[_currentLocationIndex].position.x, transitionDuration)
                .onComplete += () => _stateMachine.Switch<UpperLayerState>();
            CheckButtonActivation();
        }

        private void OnLeftMoveClick()
        {
            _stateMachine.Switch<TransitionState>();
            _currentLocationIndex =
                _currentLocationIndex - 1 < 0 ? locationsPivots.Length - 1 : _currentLocationIndex - 1;
            _camera.transform.DOMoveX(locationsPivots[_currentLocationIndex].position.x, transitionDuration)
                .onComplete += () => _stateMachine.Switch<UpperLayerState>();
            CheckButtonActivation();
        }

        private void Bind()
        {
            rightMoveButton.onClick.AddListener(OnRightMoveClick);
            leftMoveButton.onClick.AddListener(OnLeftMoveClick);
        }

        private void Expose()
        {
            rightMoveButton.onClick.RemoveListener(OnRightMoveClick);
            leftMoveButton.onClick.RemoveListener(OnLeftMoveClick);
        }
    }
}