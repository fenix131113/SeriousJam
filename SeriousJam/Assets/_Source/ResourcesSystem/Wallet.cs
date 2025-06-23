using System;
using Core;
using UnityEngine;
using VContainer.Unity;

namespace ResourcesSystem
{
    public class Wallet : ITickable
    {
        private const float PASSIVE_INCOME_PERIOD = 1f;

        public int Money { get; private set; }

        private float _passiveTimer;

        public event Action OnMoneyChanged;
        public event Action<int> OnPassiveMoneyGained;

        public void AddMoney(int amount)
        {
            Money += amount;
            OnMoneyChanged?.Invoke();
        }

        public void RemoveMoney(int amount)
        {
            Money -= amount;
            OnMoneyChanged?.Invoke();
        }

        public void Tick()
        {
            if (_passiveTimer >= PASSIVE_INCOME_PERIOD)
            {
                _passiveTimer = 0;
                var gainedMoney = (int)(1 * GameVariables.MoneyPerPassiveTick);
                Money += gainedMoney;
                
                if (gainedMoney != 0)
                {
                    OnMoneyChanged?.Invoke();
                    OnPassiveMoneyGained?.Invoke(gainedMoney);
                }
            }

            if (_passiveTimer < PASSIVE_INCOME_PERIOD)
                _passiveTimer += Time.deltaTime;
        }
    }
}