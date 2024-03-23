using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Game.Buildings
{
    public class BuildingSystem : MonoBehaviour
    {
        [SerializeField] private BuildingMenu _buildingMenu;
        private List<BuildingPosition> _buildingPositions = new();
        private BuildingPosition _selectedPosition;

        public IReadOnlyList<BuildingPosition> BuildingPositions => _buildingPositions;

        private void OnEnable()
        {
            _buildingPositions = transform.GetComponentsInChildren<BuildingPosition>().ToList();

            _buildingMenu.BuildingSelected += OnBuildingSelected;
            BuildingPosition.StartBuildingButtonPressed += OnBuildPressed;
            BuildingPosition.BuildingLoaded += OnBuildingLoaded;
            BuildingControllMenu.DestroyPressed += OnDestroyPressed;
        }

        private void OnDisable()
        {
            _buildingMenu.BuildingSelected -= OnBuildingSelected;
            BuildingPosition.StartBuildingButtonPressed -= OnBuildPressed;
            BuildingPosition.BuildingLoaded -= OnBuildingLoaded;
            BuildingControllMenu.DestroyPressed -= OnDestroyPressed;
        }

        private void OnBuildingLoaded(BuildingPosition buildingPos)
        {
            _selectedPosition = buildingPos;
            if (buildingPos.PlacedBuilding is Office office)
                AssignNeighbours(office);
        }

        private void OnBuildingSelected(Building building)
        {
            if (!_selectedPosition)
                return;

            Building built = _selectedPosition.Build(building);
            if (built is Office office)
                AssignNeighbours(office);

            _selectedPosition = null;
            _buildingMenu.Close();
        }

        private void AssignNeighbours(Office office)
        {
            if (office == null)
                return;
            int index = _buildingPositions.IndexOf(_selectedPosition);

            BuildingPosition left = index - 1 >= 0 ?
                                    _buildingPositions[index - 1] :
                                    _buildingPositions[_buildingPositions.Count - 1];
            BuildingPosition right = index + 1 < _buildingPositions.Count ?
                                     _buildingPositions[index + 1] :
                                     _buildingPositions[0];

            office.AddNeighbours(left, right);
        }

        private void OnBuildPressed(BuildingPosition buildingPosition)
        {
            _buildingMenu.Open();
            _selectedPosition = buildingPosition;
        }

        private void OnDestroyPressed(Building building)
        {
            BuildingPosition position = _buildingPositions.First(p => p.PlacedBuilding == building);
            position.DestroyBuilding();
        }
    }
}
