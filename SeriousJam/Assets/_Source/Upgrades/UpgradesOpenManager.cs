using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Upgrades.View;
using VContainer.Unity;

namespace Upgrades
{
    public class UpgradesOpenManager : IInitializable
    {
        private List<UpgradeItem> _allUpgrades;

        public void Initialize()
        {
            _allUpgrades = Object.FindObjectsByType<UpgradeItem>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .ToList();
        }

        public void CheckForUpgrades()
        {
            foreach (var upgrade in _allUpgrades)
                upgrade.CheckVisibility();
        }

        public void ClearAcceptForUpgrades()
        {
            foreach (var upgrade in _allUpgrades)
                upgrade.DeactivateAccept();
        }
    }
}