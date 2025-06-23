using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Upgrades.View;
using VContainer.Unity;

namespace Upgrades
{
    public class UpgradesOpenManager : IInitializable
    {
        private readonly List<UnityEvent> _activateOnClose = new();
        private List<UpgradeItem> _allUpgrades;

        public void Initialize()
        {
            _allUpgrades = Object.FindObjectsByType<UpgradeItem>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .ToList();
        }

        public void ActivateOnCloseRegister(UnityEvent e) => _activateOnClose.Add(e);

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

        public void InvokeAllOnClose()
        {
            foreach (var e in _activateOnClose)
                e?.Invoke();
            
            _activateOnClose.Clear();
        }
    }
}