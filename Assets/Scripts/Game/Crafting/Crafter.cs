namespace Scripts.Game.Crafting
{
    public class Crafter
    {
        public bool Craft(RecipeSO recipe)
        {
            if (!HasEnoughResources(recipe))
                return false;

            foreach (var part in recipe.Parts)
                InventoryController.Instance.Used(part.Key, part.Value);
            InventoryController.Instance.SaveItem(recipe.Result);
            return true;
        }

        private bool HasEnoughResources(RecipeSO recipe)
        {
            foreach (var part in recipe.Parts)
                if (part.Key.InventoryAmount < part.Value)
                    return false;

            return true;
        }
    }
}