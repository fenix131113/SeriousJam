using Utils.StateMachineSystem.Data;
using VContainer;

namespace World.States
{
    public class LowerLayerState : AState
    {
        [Inject] private CameraScroll _cameraScroll;
        [Inject] private NavigationPanel _navigationPanel;

        public override void OnEnter()
        {
            _cameraScroll.enabled = true;
            _navigationPanel.SetLowerInterfaceState(true);
        }

        public override void OnExit()
        {
            _cameraScroll.enabled = false;
            _navigationPanel.SetLowerInterfaceState(false);
        }
    }
}