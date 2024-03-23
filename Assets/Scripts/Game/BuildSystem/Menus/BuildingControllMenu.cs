using Scripts.Game.Buildings;
using Scripts.OnUI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game
{
    public class BuildingControllMenu : Menu
    {
        public static event Action<Building> DestroyPressed;

        [SerializeField] private TextMeshProUGUI _nameField;
        [SerializeField] private TextMeshProUGUI _currentValue;
        [SerializeField] private TextMeshProUGUI _nextValue;
        [SerializeField] private TextMeshProUGUI _priceField;
        [SerializeField] private TextMeshProUGUI _statsText;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private PopUpScreen _popUp;
        private Building _representing;

        public void ShowHelp()
        {
            _popUp.Init(_representing.Data.Name, _representing.Data.Description);
            _popUp.gameObject.SetActive(true);
        }

        public void Open(Building building)
        {
            Init(building);
            Open();
        }

        public void Upgrade()
        {
            if (_representing == null)
                return;

            if (PlayerCoins.Instance.TryWithdraw(_representing.PriceForUpgrade))
                _representing.Upgrade();
            else
            {
                _popUp.InitFailed();
                _popUp.gameObject.SetActive(true);
            }
        }

        public void DestroyBuild()
        {
            _representing.Upgraded -= UpdateFields;
            DestroyPressed?.Invoke(_representing);
            _representing = null;
        }

        public void Init(Building building)
        {
            if (_representing != null)
                _representing.Upgraded -= UpdateFields;

            _representing = building;
            _representing.Upgraded += UpdateFields;
            UpdateFields();
        }

        private void OnEnable()
        {
            Building.Selected += Open;
        }

        private void OnDisable()
        {
            if (_representing != null)
                _representing.Upgraded -= UpdateFields;
            Building.Selected -= Open;
        }

        private void UpdateFields()
        {
            _nameField.text = _representing.Data.Name;
            _currentValue.text = _representing.GetValue.ToString();

            int upgradePrice = _representing.PriceForUpgrade;
            _statsText.text = _representing.GetStats();
            if (upgradePrice == -1)
            {
                _priceField.text = "MAX";
                _upgradeButton.interactable = false;
                _nextValue.text = "MAX";
            }
            else
            {
                _upgradeButton.interactable = true;
                _priceField.text = upgradePrice.ToString();
                _nextValue.text = _representing.GetNextValue.ToString();
            }
        }
    }
}