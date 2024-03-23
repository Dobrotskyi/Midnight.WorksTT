using Scripts.Game.SaveSystem;
using Scripts.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game.Crafting
{
    public class InventoryController : IPersistentData
    {
        public event Action<ItemSO> AmountChanged;
        public static InventoryController Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new InventoryController();
                GameDataManager.Instance.LoadData(s_instance);
                return s_instance;
            }
        }

        private static InventoryController s_instance;
        private InventoryController() { _items = new(); }

        private Dictionary<string, ItemInfo> _items;

        public void SaveItem(ItemSO item)
        {
            if (_items.ContainsKey(item.Name))
            {
                ItemInfo info = _items[item.Name];
                info.Amount += 1;
                _items[item.Name] = info;
            }
            else
                _items.Add(item.Name, new(item.Name, 1));
            GameDataManager.Instance.SaveData(new[] { this });
            AmountChanged?.Invoke(item);
        }

        public int GetAmount(ItemSO item)
        {
            if (_items.ContainsKey(item.Name))
                return _items[item.Name].Amount;
            return 0;
        }

        public void Used(ItemSO item, int amount)
        {
            if (amount <= 0)
            {
                Debug.LogWarning("Amount can`t be equal or less than 0");
                return;
            }
            int inventoryAmount = GetAmount(item);
            if (amount > inventoryAmount)
            {
                Debug.LogWarning("Amount of items to use can`t be bigger than actual amount");
                return;
            }

            ItemInfo info = _items[item.Name];
            info.Amount -= amount;
            _items[item.Name] = info;
            GameDataManager.Instance.SaveData(new[] { this });
            AmountChanged?.Invoke(item);
        }

        public void LoadDataFrom(GameData data)
        {
            _items = data.LoadInventory();
        }

        public void SaveDataTo(GameData data)
        {
            foreach (var item in _items)
                data.SaveItem(item.Value);
        }
    }
}