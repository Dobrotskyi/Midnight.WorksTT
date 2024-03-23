using Scripts.Game.SaveSystem;
using System;

namespace Scripts.Game
{
    public class PlayerCoins : IPersistentData
    {
        private static PlayerCoins s_instance;
        private PlayerCoins() { }

        public static PlayerCoins Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new();
                GameDataManager.Instance.LoadData(s_instance);
                return s_instance;
            }
        }

        public static event Action Changed;

        private int _amount;

        public int Amount
        {
            private set
            {
                if (value < 0)
                    value = 0;
                _amount = value;
                Changed?.Invoke();
            }
            get => _amount;
        }

        public bool TryWithdraw(int amount)
        {
            if (amount < 0) return false;
            if (amount > _amount) return false;

            Amount -= amount;
            GameDataManager.Instance.SaveData(new[] { this });
            return true;
        }

        public void Add(int amount)
        {
            if (amount < 0) amount = 0;
            Amount += amount;
            GameDataManager.Instance.SaveData(new[] { this });
        }

        public void LoadDataFrom(GameData data)
        {
            Amount = data.CoinsAmount;
        }

        public void SaveDataTo(GameData data)
        {
            data.CoinsAmount = Amount;
        }
    }
}