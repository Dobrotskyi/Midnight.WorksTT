using UnityEngine;

namespace Scripts.Game.Crafting
{
    [CreateAssetMenu(menuName = "ScriptableObject/Item", fileName = "Item")]
    public class ItemSO : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [TextArea]
        [SerializeField] private string _description;

        public Sprite Icon => _icon;
        public string Name => _name;
        public string Description => _description;
        public int InventoryAmount => InventoryController.Instance.GetAmount(this);
    }
}