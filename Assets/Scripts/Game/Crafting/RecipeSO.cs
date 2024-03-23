using Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game.Crafting
{
    [CreateAssetMenu(menuName = "ScriptableObject/Recepie", fileName = "Recepie")]
    public class RecipeSO : ScriptableObject
    {
        [SerializeField] private List<SerializableKeyValue<ItemSO, int>> _parts;
        [SerializeField] private ItemSO _result;

        public IReadOnlyList<SerializableKeyValue<ItemSO, int>> Parts => _parts;
        public ItemSO Result => _result;
    }
}