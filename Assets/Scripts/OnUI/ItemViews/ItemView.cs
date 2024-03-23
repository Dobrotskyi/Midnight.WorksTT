using Scripts.Game.Crafting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.OnUI
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [Header("Can be left null")]
        [SerializeField] private TextMeshProUGUI _nameField;
        [Header("Can be left null")]
        [SerializeField] private TextMeshProUGUI _amountField;
        private ItemSO _data;

        public ItemSO Data => _data;

        public void Init(ItemSO item, int amount = -1)
        {
            _data = item;
            _image.sprite = _data.Icon;
            if (_nameField != null)
                _nameField.text = _data.Name;
            if (_amountField != null)
            {
                if (amount == -1)
                {
                    _amountField.text = _data.InventoryAmount.ToString();
                    InventoryController.Instance.AmountChanged += OnAmountChanged;
                }
                else
                    _amountField.text = amount.ToString();
            }
        }

        private void OnDestroy()
        {
            InventoryController.Instance.AmountChanged -= OnAmountChanged;
        }

        private void OnAmountChanged(ItemSO item)
        {
            if (item != _data)
                return;
            _amountField.text = _data.InventoryAmount.ToString();
        }
    }
}