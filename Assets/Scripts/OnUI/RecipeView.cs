using Scripts.Game.Crafting;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.OnUI
{
    public class RecipeView : MonoBehaviour
    {
        public event Action<RecipeSO> Selected;

        [SerializeField] private HorizontalLayoutGroup _componentsGroup;
        [SerializeField] private Image _resultIcon;
        [SerializeField] private TextMeshProUGUI _resultText;
        [SerializeField] private ItemView _partView;
        private RecipeSO _representing;

        public void Init(RecipeSO recipe)
        {
            _representing = recipe;
            foreach (var part in _representing.Parts)
            {
                ItemView view = Instantiate(_partView, _componentsGroup.transform);
                view.Init(part.Key, part.Value);
            }

            _resultIcon.sprite = _representing.Result.Icon;
            _resultText.text = _representing.Result.Name;
        }

        public void Pressed()
        {
            Selected?.Invoke(_representing);
        }
    }
}