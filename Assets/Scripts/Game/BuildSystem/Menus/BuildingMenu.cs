using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Game.Buildings
{
    public class BuildingMenu : Menu
    {
        public event Action<Building> BuildingSelected;

        [SerializeField] private RectTransform _buttonGroup;
        private List<BuildingButton> _buildingButtons = new();

        private void OnEnable()
        {
            _buildingButtons = _buttonGroup.GetComponentsInChildren<BuildingButton>().ToList(); ;

            foreach (var button in _buildingButtons)
                button.Selected += OnBuildingButtonClick;
        }

        private void OnDisable()
        {
            foreach (var button in _buildingButtons)
                button.Selected -= OnBuildingButtonClick;
        }

        private void OnBuildingButtonClick(Building building) => BuildingSelected?.Invoke(building);
    }
}