using Scripts.OnUI;
using System;
using TMPro;
using UnityEngine;

namespace Scripts.Game.Buildings
{
    public class BuildingButton : MonoBehaviour
    {
        public event Action<Building> Selected;
        [SerializeField] private Building _building;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _priceField;
        [SerializeField] private PopUpScreen _popUp;

        public void Select()
        {
            if (PlayerCoins.Instance.Amount < _building.Data.PriceForBuild)
            {
                _popUp.InitFailed();
                _popUp.gameObject.SetActive(true);
            }
            else
                Selected?.Invoke(_building);
        }

        public void ShowHelp()
        {
            _popUp.Init(_building.Data.Name, _building.Data.Description);
            _popUp.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            _name.text = _building.Data.Name;
            _priceField.text = _building.Data.PriceForBuild.ToString();
        }
    }
}