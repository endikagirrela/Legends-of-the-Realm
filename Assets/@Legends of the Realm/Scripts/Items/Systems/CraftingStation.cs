using System.Collections;
using UnityEngine;
using Game.Utilities;

public class CraftingStation : MonoBehaviour
{
    public CraftingStationType stationType;

    public IEnumerator Craft(CraftingRecipe recipe, Inventory playerInventory, System.Action<bool> onComplete)
    {
        if (recipe.stationRequired != stationType)
        {
            onComplete?.Invoke(false);
            yield break;
        }

        if (!CraftingUtils.HasMaterials(playerInventory, recipe))
        {
            onComplete?.Invoke(false);
            yield break;
        }

        // Inicio de crafteo (puedes mostrar UI/progreso)
        float elapsed = 0f;
        while (elapsed < recipe.craftTime)
        {
            elapsed += Time.deltaTime;
            // actualizar UI si hace falta
            yield return null;
        }

        // Determinar éxito
        bool success = Random.value <= recipe.successChance;

        if (success)
        {
            // Consumir materiales y dar item
            CraftingUtils.ConsumeMaterials(playerInventory, recipe);
            playerInventory.AddItem(recipe.resultItem);
        }
        else
        {
            if (recipe.consumesMaterialsOnFail)
                CraftingUtils.ConsumeMaterials(playerInventory, recipe);
            // Opcional: feedback de fallo
        }

        onComplete?.Invoke(success);
    }
}
