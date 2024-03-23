using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game.Buildings
{
    [CreateAssetMenu(menuName = "ScriptableObject/PgorgessionConfig", fileName = "ProgressionConfig")]
    public class ProgressConfigSO : ScriptableObject
    {
        [SerializeField] private int _maxLevel = 5;
        [SerializeField] private List<float> _valuePerLevel;
        [SerializeField] private List<int> _priceForNext;

        public int MaxLevel => _maxLevel;

        public float ValueOnLevel(int level)
        {
            if (level > _maxLevel) level = _maxLevel;
            if (level < 0) level = 0;
            return _valuePerLevel[level - 1];
        }

        public int PriceOnLevel(int level)
        {
            if (level >= _maxLevel) level = _maxLevel - 1;
            if (level < 0) level = 0;
            return _priceForNext[level];
        }

        private void OnValidate()
        {
            if (_valuePerLevel.Count < _maxLevel)
            {
                _valuePerLevel.Capacity = _maxLevel;
                Debug.LogWarning("Fill in all the value progression");
            }

            if (_priceForNext.Count < _maxLevel)
            {
                _priceForNext.Capacity = _maxLevel;
                Debug.LogWarning("Fill in all the price progression");
            }
        }
    }
}