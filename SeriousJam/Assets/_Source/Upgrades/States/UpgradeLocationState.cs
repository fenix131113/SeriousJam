using Upgrades.View;
using Utils.StateMachineSystem.Data;
using VContainer;

namespace Upgrades.States
{
    public class UpgradeLocationState : AState
    {
        [Inject] private UpgradePanel _upgradePanel;

        public override void OnEnter()
        {
            _upgradePanel.SetUpgradeButtonActive(false);
            _upgradePanel.SetCloseButtonActive(true);
        }

        public override void OnExit()
        {
            _upgradePanel.SetCloseButtonActive(false);
        }
    }
}