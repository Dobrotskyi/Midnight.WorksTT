using System;
using UnityEngine;

namespace Scripts.Utils
{
    [Serializable]
    public struct ItemInfo
    {
        [SerializeField] private string _name;
        [SerializeField] private int _amount;

        public string Name => _name;
        public int Amount
        {
            set
            {
                if (value < 0)
                    value = 0;
                _amount = value;
            }
            get => _amount;
        }

        public ItemInfo(string name, int amount)
        {
            _name = name;
            _amount = amount;
        }
    }
}