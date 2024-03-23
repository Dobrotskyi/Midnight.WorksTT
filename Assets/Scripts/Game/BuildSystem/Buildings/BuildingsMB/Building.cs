using Scripts.Input;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Game.Buildings
{
    public abstract class Building : MonoBehaviour
    {
        public static event Action<Building> Selected;
        public event Action Upgraded;

        [SerializeField] private BuildingSO _buildingSO;

        private int _currentLevel = 1;
        public float GetValue => _buildingSO.ProgressConfig.ValueOnLevel(CurrentLevel);
        public BuildingSO Data => _buildingSO;

        public string Id => Data.Id;

        public int CurrentLevel
        {
            private set
            {
                if (value < 1 || CurrentLevel == Data.ProgressConfig.MaxLevel)
                    return;
                if (value > _buildingSO.ProgressConfig.MaxLevel)
                    value = _buildingSO.ProgressConfig.MaxLevel;
                _currentLevel = value;
                Upgraded?.Invoke();
            }
            get => _currentLevel;
        }

        public int PriceForUpgrade
        {
            get
            {
                if (CurrentLevel == Data.ProgressConfig.MaxLevel)
                    return -1;
                else
                    return Data.ProgressConfig.PriceOnLevel(CurrentLevel);
            }
        }

        public float GetNextValue
        {
            get
            {
                if (CurrentLevel == Data.ProgressConfig.MaxLevel)
                    return -1;
                else
                    return Data.ProgressConfig.ValueOnLevel(CurrentLevel + 1);
            }
        }

        public abstract string GetStats();

        public void OpenUpgradeMenu() => Selected?.Invoke(this);

        public void Upgrade() => CurrentLevel++;

        public void SetLevel(int level) => CurrentLevel = level;

        private void OnMouseDown()
        {
            if (!PlayerInputUtils.PointIsOnUI(Mouse.current.position.ReadValue()))
                Selected?.Invoke(this);
        }
    }
}