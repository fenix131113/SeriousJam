using Upgrades.View;
using Utils.StateMachineSystem.Data;
using VContainer;

namespace Upgrades.States
{
    public class UpgradeLocationState : AState
    {
        [Inject] private UpgradesOpenManager _upgradesOpenManager;
        [Inject] private UpgradePanel _upgradePanel;
        [Inject] private InfoPanel _infoPanel;

        public override void OnEnter()
        {
            _upgradePanel.SetUpgradeButtonActive(false);
            _upgradePanel.SetCloseButtonActive(true);
        }

        public override void OnExit()
        {
            _upgradesOpenManager.ClearAcceptForUpgrades();
            _upgradePanel.SetCloseButtonActive(false);
            _infoPanel.ClosePanel();
        }
    }
}