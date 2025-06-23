using System;
using Core;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace ResourcesSystem
{
    public class Clicker : MonoBehaviour
    {
        [Inject] private Wallet _wallet;
        private Button _clickerButton;

        public event Action<int> OnClickGained;

        private void Start()
        {
            _clickerButton = GetComponent<Button>();
            Bind();
        }

        private void OnDestroy() => Expose();

        private void OnClick()
        {
            var money = (int)(1 * GameVariables.MoneyPerClickMultiplier);

            _wallet.AddMoney(money);
            OnClickGained?.Invoke(money);
        }

        private void Bind() => _clickerButton.onClick.AddListener(OnClick);

        private void Expose() => _clickerButton.onClick.RemoveListener(OnClick);
    }
}