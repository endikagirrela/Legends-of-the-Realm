namespace Game.Utilities
{
    public static class CraftingUtils
    {
        public static bool HasMaterials(Inventory inv, CraftingRecipe recipe)
        {
            foreach (var req in recipe.materials)
            {
                int have = inv.GetMaterialCount(req.material);
                if (have < req.amount) return false;
            }
            return true;
        }

        public static void ConsumeMaterials(Inventory inv, CraftingRecipe recipe)
        {
            foreach (var req in recipe.materials)
            {
                inv.RemoveMaterial(req.material, req.amount);
            }
        }
    }
}
