using Scripts.Game.Buildings;
using Scripts.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game.SaveSystem
{
    [System.Serializable]
    public class GameData
    {
        [Serializable]
        public struct BuildingInfo
        {
            [SerializeField] private string _posId;
            [SerializeField] private string _buildingID;
            [SerializeField] private int _buildingLevel;

            public string PosId => _posId;
            public string BuildingID => _buildingID;
            public int BuildingLevel => _buildingLevel;

            public BuildingInfo(string posId, string buildingId, int level)
            {
                _posId = posId;
                _buildingID = buildingId;
                _buildingLevel = level;
            }

            public override bool Equals(object obj)
            {
                if (obj == null || !(obj is BuildingInfo))
                    return false;
                BuildingInfo other = (BuildingInfo)obj;
                if (_posId != other._posId)
                    return false;
                if (_buildingID != other._buildingID)
                    return false;
                if (_buildingLevel != other._buildingLevel)
                    return false;
                return true;
            }

            public override int GetHashCode()
            { return _posId.GetHashCode(); }
        }

        public GameData()
        {
            _positionAndBuilding = new();
            _itemInfos = new();
            _coinsAmount = 1000;
        }
        [SerializeField] private SerializableDictionary<string, BuildingInfo> _positionAndBuilding;
        [SerializeField] private SerializableDictionary<string, ItemInfo> _itemInfos;
        [SerializeField] private int _coinsAmount;

        public int CoinsAmount
        {
            set
            {
                if (value < 0)
                    value = 0;
                _coinsAmount = value;
            }
            get => _coinsAmount;
        }

        public void SavePosition(BuildingPosition position)
        {
            if (_positionAndBuilding.ContainsKey(position.Id))
                _positionAndBuilding.Remove(position.Id);
            if (position.PlacedBuilding == null)
                return;
            _positionAndBuilding.Add(position.Id,
                                    new(position.Id, position.PlacedBuilding.Id,
                                    position.PlacedBuilding.CurrentLevel));
        }

        public BuildingInfo? LoadPosition(BuildingPosition position)
        {
            _positionAndBuilding.TryGetValue(position.Id,
                                             out BuildingInfo result);
            if (result.Equals(default(BuildingInfo)))
                return null;
            return result;
        }

        public void SaveItem(ItemInfo info)
        {
            if (_itemInfos.ContainsKey(info.Name))
                _itemInfos.Remove(info.Name);


            _itemInfos.Add(info.Name, new(info.Name, info.Amount));
        }

        public Dictionary<string, ItemInfo> LoadInventory() => new Dictionary<string, ItemInfo>(_itemInfos);
    }
}
