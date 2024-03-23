using Scripts.Game;
using TMPro;
using UnityEngine;

namespace Scripts.OnUI
{
    public class PlayerCoinsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _amountField;
        PlayerCoins _playerCoins;

        private void Awake()
        {
            _playerCoins = PlayerCoins.Instance;
        }

        private void OnEnable()
        {
            UpdateField();
            PlayerCoins.Changed += UpdateField;
        }

        private void OnDisable()
        {
            PlayerCoins.Changed -= UpdateField;
        }

        private void UpdateField() => _amountField.text = _playerCoins.Amount.ToString();
    }
}