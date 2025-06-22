using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using World.States;

namespace World
{
    public class LayersTransition : MonoBehaviour
    {
        [SerializeField] private Transform upperLayerPivot;
        [SerializeField] private Transform lowerLayerPivot;
        [SerializeField] private Button upperLayerButton;
        [SerializeField] private Button lowerLayerButton;
        [SerializeField] private float transitionDuration = 0.5f;

        [Inject] private WorldTransitionStateMachine _stateMachine;
        private Camera _camera;
        private void Awake() => _camera = Camera.main;

        private void Start() => Bind();

        private void OnDestroy() => Expose();

        private void GoToUpperLayer()
        {
            _stateMachine.Switch<TransitionState>();
            _camera.transform.DOMoveY(upperLayerPivot.position.y, transitionDuration).onComplete +=
                () => _stateMachine.Switch<UpperLayerState>();
        }

        private void GoToLowerLayer()
        {
            _stateMachine.Switch<TransitionState>();
            _camera.transform.DOMoveY(lowerLayerPivot.position.y, transitionDuration).onComplete +=
                () => _stateMachine.Switch<LowerLayerState>();
        }

        private void Bind()
        {
            upperLayerButton.onClick.AddListener(GoToUpperLayer);
            lowerLayerButton.onClick.AddListener(GoToLowerLayer);
        }

        private void Expose()
        {
            upperLayerButton.onClick.RemoveListener(GoToUpperLayer);
            lowerLayerButton.onClick.RemoveListener(GoToLowerLayer);
        }
    }
}