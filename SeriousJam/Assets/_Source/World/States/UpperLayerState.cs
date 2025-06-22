using Utils.StateMachineSystem.Data;
using VContainer;

namespace World.States
{
    public class UpperLayerState : AState
    {
        [Inject] private NavigationPanel _navigationPanel;
        [Inject] private UpperLayerNavigation _upperNavigation;
        
        public override void OnEnter()
        {
            _navigationPanel.SetUpperLayerPanelState(true);
            _upperNavigation.CheckButtonActivation();
        }

        public override void OnExit() => _navigationPanel.SetUpperLayerPanelState(false);
    }
}