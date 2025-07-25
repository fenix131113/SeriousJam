﻿using Upgrades.View;
using Utils.StateMachineSystem.Data;
using VContainer;

namespace World.States
{
    public class TransitionState : AState
    {
        [Inject] private UpgradePanel _upgradePanel;

        public override void OnEnter()
        {
            _upgradePanel.SetUpgradeButtonActive(false);
        }

        public override void OnExit()
        {
            _upgradePanel.SetUpgradeButtonActive(true);
        }
    }
}