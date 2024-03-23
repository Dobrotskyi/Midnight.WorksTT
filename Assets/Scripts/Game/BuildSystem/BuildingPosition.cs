using Scripts.Game.SaveSystem;
using System;
using UnityEngine;

namespace Scripts.Game.Buildings
{
    public class BuildingPosition : MonoBehaviour, IPersistentData
    {
        public static event Action<BuildingPosition> StartBuildingButtonPressed;
        public static event Action<BuildingPosition> BuildingLoaded;


        public event Action BuildingChanged;

        [SerializeField] private GameObject _selectPosButton;
        [SerializeField] private string _id = string.Empty;

        public Building PlacedBuilding { private set; get; }
        public string Id => _id;
        public void Pressed() => StartBuildingButtonPressed?.Invoke(this);

        public Building Build(Building building, int level = 0, bool withSave = true)
        {
            if (PlacedBuilding != null)
                return PlacedBuilding;

            PlacedBuilding = Instantiate(building, transform);
            PlacedBuilding.transform.localRotation = Quaternion.identity;
            if (level > 0)
                PlacedBuilding.SetLevel(level);
            if (withSave)
                SaveChanges();

            BuildingChanged?.Invoke();
            PlacedBuilding.Upgraded += OnUpgraded;
            _selectPosButton.SetActive(false);

            return PlacedBuilding;
        }

        public void DestroyBuilding()
        {
            _selectPosButton.SetActive(true);
            PlacedBuilding.Upgraded -= OnUpgraded;
            Destroy(PlacedBuilding.gameObject);
            PlacedBuilding = null;
            BuildingChanged?.Invoke();
            SaveChanges();
        }

        private void OnUpgraded()
        {
            BuildingChanged?.Invoke();
            SaveChanges();
        }

        public void LoadDataFrom(GameData data)
        {
            GameData.BuildingInfo? buildingInfo = data.LoadPosition(this);
            if (buildingInfo == null) return;
            Building prefab = BuildingsCatalog.Instance.
                  GetBuilding(buildingInfo.Value.BuildingID).Prefab;
            Build(prefab, buildingInfo.Value.BuildingLevel, false);
            BuildingLoaded?.Invoke(this);
        }

        public void SaveDataTo(GameData data)
        {
            data.SavePosition(this);
        }

        private void SaveChanges() => GameDataManager.Instance.SaveData(new[] { this });

        [ContextMenu("Generate ID")]
        private void GenerateGUID()
        {
            _id = Guid.NewGuid().ToString();
        }
    }
}