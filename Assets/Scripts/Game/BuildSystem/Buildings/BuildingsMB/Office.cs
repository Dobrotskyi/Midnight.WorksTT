using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game.Buildings
{
    public class Office : Building
    {
        public static event Action<int> Finished;

        private const int BASIC_WORKERS_AMT = 2;
        private const float BASIC_INCOME_RATE_IN_SEC = 4;

        [SerializeField] private Image _progressImage;
        private HashSet<BuildingPosition> _neighboursPosition;
        private float _secondsBeforeIncome = BASIC_INCOME_RATE_IN_SEC;
        private int _workersAmount = BASIC_WORKERS_AMT;

        public new int GetValue => Mathf.FloorToInt(base.GetValue * _workersAmount);

        public override string GetStats()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Workers capacity: {_workersAmount}");
            sb.AppendLine($"Income delay: {_secondsBeforeIncome}");
            sb.AppendLine($"Total income: {GetValue}");
            return sb.ToString();
        }

        public void AddNeighbours(BuildingPosition first, BuildingPosition second)
        {
            if (_neighboursPosition != null && _neighboursPosition.Count > 0)
            {
                foreach (var pos in _neighboursPosition)
                    pos.BuildingChanged -= CountBonuses;
                _neighboursPosition.Clear();
            }

            _neighboursPosition = new() { first, second };
            foreach (var pos in _neighboursPosition)
                pos.BuildingChanged += CountBonuses;

            CountBonuses();
        }

        private void OnDestroy()
        {
            if (_neighboursPosition != null)
                foreach (var pos in _neighboursPosition)
                    pos.BuildingChanged -= CountBonuses;
        }

        private void CountBonuses()
        {
            CountIncomeRate();
            CountWorkers();
        }

        private void CountIncomeRate()
        {
            _secondsBeforeIncome = BASIC_INCOME_RATE_IN_SEC;
            foreach (Building building in _neighboursPosition.Select(p => p.PlacedBuilding))
                if (building is CoffeeShop coffeeShop)
                    _secondsBeforeIncome -= coffeeShop.GetValue;

            if (_secondsBeforeIncome <= 0f)
            {
                Debug.LogError("Value can`t be 0 or less");
                _secondsBeforeIncome = 0.1f;
            }
        }

        private void CountWorkers()
        {
            _workersAmount = BASIC_WORKERS_AMT;
            foreach (Building building in _neighboursPosition.Select(p => p.PlacedBuilding))
                if (building is Hotel hotel)
                    _workersAmount += hotel.GetValue;
        }

        private void OnEnable()
        {
            StartCoroutine(Work());
        }

        private IEnumerator Work()
        {
            float progress = 0f;
            float time = 0f;
            while (true)
            {
                time += Time.deltaTime;
                progress = time / _secondsBeforeIncome;
                _progressImage.fillAmount = progress;
                if (progress >= 1f)
                {
                    Finished?.Invoke(GetValue);
                    progress = 0f;
                    time = 0f;
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}