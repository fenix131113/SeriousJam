using System;
using System.Collections.Generic;
using Core;
using ResourcesSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VContainer;

namespace Upgrades.View
{
    public class UpgradeItem : MonoBehaviour
    {
        [SerializeField] private List<UpgradeItem> needToUnlock = new();
        [SerializeField] private List<UpgradeItemGroup> upgrades = new();
        [SerializeField] private TMP_Text costLabel;
        [SerializeField] private TMP_Text levelLabel;

        public int Level { get; private set; }
        public int MaxLevel => upgrades.Count;

        [Inject] private Wallet _wallet;
        [Inject] private UpgradesOpenManager _upgradesOpenManager;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();

            Bind();
            DrawInfo();
        }

        private void OnDestroy() => Expose();

        public void CheckVisibility()
        {
            if (!needToUnlock.TrueForAll(x => x.Level > 0))
                return;

            gameObject.SetActive(true);
        }

        private void OnUpgradeClicked()
        {
            if (Level == MaxLevel || _wallet.Money < upgrades[Level].cost || !needToUnlock.TrueForAll(x => x.Level > 0))
                return;

            if (upgrades[Level].increasePassive)
                GameVariables.SetPassivePerTick(GameVariables.MoneyPerPassiveTick +
                                                upgrades[Level].passiveIncreaseAmount);
            if (upgrades[Level].increaseClick)
                GameVariables.SetMoneyPerClick(GameVariables.MoneyPerClickMultiplier +
                                                upgrades[Level].clickIncreaseAmount);

            _wallet.RemoveMoney(upgrades[Level].cost);
            upgrades[Level].onLevelUpgraded?.Invoke();
            Level++;
            _upgradesOpenManager.CheckForUpgrades();
            DrawInfo();
        }

        private void DrawInfo()
        {
            if (Level == MaxLevel)
                costLabel.gameObject.SetActive(false);
            else
                costLabel.text = upgrades[Level].cost.ToString();

            levelLabel.text = $"{Level}/{MaxLevel}";
        }

        private void Bind() => _button.onClick.AddListener(OnUpgradeClicked);

        private void Expose() => _button.onClick.RemoveAllListeners();
    }

    [Serializable]
    public class UpgradeItemGroup
    {
        [field: SerializeField] public UnityEvent onLevelUpgraded;
        [field: SerializeField] public int cost;
        [field: SerializeField] public bool increasePassive;
        [field: SerializeField] public float passiveIncreaseAmount;
        [field: SerializeField] public bool increaseClick;
        [field: SerializeField] public float clickIncreaseAmount;
    }
}