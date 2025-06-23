using TMPro;
using UnityEngine;
using VContainer;

namespace ResourcesSystem.View
{
    public class MoneyCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text amountLabel;

        [Inject] private Wallet _wallet;

        private void Start() => Bind();

        private void OnDestroy() => Expose();

        private void DrawMoney() => amountLabel.text = _wallet.Money.ToString();

        private void Bind() => _wallet.OnMoneyChanged += DrawMoney;

        private void Expose() => _wallet.OnMoneyChanged -= DrawMoney;
    }
}