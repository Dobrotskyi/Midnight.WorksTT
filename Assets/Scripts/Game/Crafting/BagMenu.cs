using Scripts.OnUI;
using Scripts.Utils.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game.Crafting
{
    public class BagMenu : Menu
    {
        [SerializeField] private VerticalLayoutGroup _recipeGroup;
        [SerializeField] private VerticalLayoutGroup _inventoryGroup;
        [SerializeField] private RecipeView _recipeView;
        [SerializeField] private InventoryItemView _fullInfoView;
        [SerializeField] private PopUpScreen _popUp;
        private List<RecipeSO> _recipes;

        private List<InventoryItemView> _inventoryViews;
        private List<RecipeView> _recipeViews;

        private void Awake()
        {
            _recipes = ScriptableObjectExtensions<RecipeSO>.GetAllInstances().ToList();
            FillRecipeView();
            FillInventoryView();
            InventoryController.Instance.AmountChanged += OnAmountChanged;
        }

        private void OnDestroy()
        {
            _inventoryViews.ForEach(v => v.Clicked -= ShowHelp);
            _recipeViews.ForEach(v => v.Selected -= OnRecipeSelected);
            InventoryController.Instance.AmountChanged -= OnAmountChanged;
        }

        private void FillRecipeView()
        {
            _recipeViews = new();
            foreach (var recipe in _recipes)
            {
                RecipeView view = Instantiate(_recipeView, _recipeGroup.transform);
                view.Init(recipe);
                _recipeViews.Add(view);
                view.Selected += OnRecipeSelected;
            }
        }

        private void OnRecipeSelected(RecipeSO recipe)
        {
            Crafter crafter = new();
            if (!crafter.Craft(recipe))
            {
                _popUp.InitFailed();
                _popUp.gameObject.SetActive(true);
            }
        }

        private void FillInventoryView()
        {
            var allItems = ScriptableObjectExtensions<ItemSO>.GetAllInstances();
            _inventoryViews = new();
            foreach (var item in allItems)
            {
                InventoryItemView view = Instantiate(_fullInfoView, _inventoryGroup.transform);
                view.Init(item);
                _inventoryViews.Add(view);
                view.Clicked += ShowHelp;
                if (item.InventoryAmount == 0)
                    view.gameObject.SetActive(false);
            }
        }

        private void ShowHelp(ItemSO item)
        {
            _popUp.Init(item.Name, item.Description);
            _popUp.gameObject.SetActive(true);
        }

        private void OnAmountChanged(ItemSO item)
        {
            ItemView match = _inventoryViews.First(v => v.Data == item);
            if (match.gameObject.activeSelf)
            {
                if (match.Data.InventoryAmount == 0)
                    match.gameObject.SetActive(false);
            }
            else
            {
                if (match.Data.InventoryAmount > 0)
                    match.gameObject.SetActive(true);
            }
        }
    }
}