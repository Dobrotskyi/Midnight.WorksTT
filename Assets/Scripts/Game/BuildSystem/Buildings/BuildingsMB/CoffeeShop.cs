using Scripts.Game.Crafting;
using Scripts.OnUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game.Buildings
{
    public class CoffeeShop : Building
    {
        [SerializeField] private List<ItemSO> _avaliableItems = new();
        [SerializeField] private Image _progressImage;
        [SerializeField] private ItemView _itemView;
        [SerializeField] private Canvas _canvas;
        private Vector2 _spawnRange = new(2, 5);

        public override string GetStats()
        {
            string stats = $"Decreases income delay in nearby offices by {GetValue}";
            return stats;
        }

        private void OnEnable()
        {
            StartCoroutine(Work());
        }

        private IEnumerator Work()
        {
            float time = 0;
            float progress = 0;
            float timeBeforeSpawn = Random.Range(_spawnRange.x, _spawnRange.y);
            while (true)
            {
                time += Time.deltaTime;
                progress = time / timeBeforeSpawn;
                _progressImage.fillAmount = progress;
                if (progress >= 1f)
                {
                    CreateItem();
                    progress = 0f;
                    time = 0f;
                    timeBeforeSpawn = Random.Range(_spawnRange.x, _spawnRange.y);
                }
                yield return new WaitForEndOfFrame();
            }
        }

        private void CreateItem()
        {
            ItemSO randomSO = _avaliableItems[Random.Range(0, _avaliableItems.Count)];
            ItemView view = Instantiate(_itemView, _canvas.transform);
            view.Init(randomSO);
            InventoryController.Instance.SaveItem(randomSO);
        }
    }
}