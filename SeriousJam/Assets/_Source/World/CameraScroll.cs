using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace World
{
    public class CameraScroll : MonoBehaviour
    {
        [SerializeField] private float cameraScrollSpeed = 0.5f;
        [SerializeField] private float maxCameraY;
        [SerializeField] private float minCameraY;

        [Inject] private InputSystem_Actions _input;
        private Camera _camera;

        private void Awake() => _camera = Camera.main;

        private void Start() => Bind();

        private void OnDestroy() => Expose();

        private void OnPlayerScroll(InputAction.CallbackContext obj)
        {
            if(!enabled)
                return;
            
            var scroll = obj.ReadValue<Vector2>();

            _camera.transform.position += Vector3.up * (scroll.y * cameraScrollSpeed);
            _camera.transform.position = new Vector3(_camera.transform.position.x,
                Mathf.Clamp(_camera.transform.position.y, minCameraY, maxCameraY), _camera.transform.position.z);
        }

        private void Bind() => _input.Player.Scroll.performed += OnPlayerScroll;

        private void Expose() => _input.Player.Scroll.performed -= OnPlayerScroll;
    }
}