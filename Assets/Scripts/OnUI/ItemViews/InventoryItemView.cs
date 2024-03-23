using Scripts.Game.Crafting;
using System;

namespace Scripts.OnUI
{
    public class InventoryItemView : ItemView
    {
        public event Action<ItemSO> Clicked;

        public void OnClick()
        {
            Clicked?.Invoke(Data);
        }
    }
}