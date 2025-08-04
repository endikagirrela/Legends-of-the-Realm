using System.Collections;
using UnityEngine;

public class SmeltingStation : MonoBehaviour
{
    public SmeltRecipe currentRecipe;
    public int inputAmount = 1; // cu�ntas menas se procesan a la vez
    public Inventory outputInventory; // donde va el mineral fundido (puede ser el mismo Inventory del jugador)
    public float progress { get; private set; } = 0f;
    public bool IsProcessing { get; private set; } = false;

    // Llamar para iniciar fundici�n
    public void StartSmelt(SmeltRecipe recipe, int amount = 1)
    {
        if (IsProcessing) return;
        currentRecipe = recipe;
        inputAmount = amount;
        StartCoroutine(ProcessSmelt());
    }

    private IEnumerator ProcessSmelt()
    {
        if (currentRecipe == null) yield break;

        IsProcessing = true;
        progress = 0f;
        float duration = currentRecipe.smeltTimeSeconds;

        // Aqu� puedes consumir la mena del inventario si tienes referencia
        while (progress < duration)
        {
            progress += Time.deltaTime;
            // Opcional: actualizar UI con progress / duration
            yield return null;
        }

        // Al completarse, dar el material
        for (int i = 0; i < inputAmount; i++)
        {
            // Agrega outputMaterial al inventario (puedes envolverlo en un ItemData si lo manejas como �tem)
            // Ejemplo simplificado: suponiendo que MaterialData se puede a�adir directamente
            // Si usas un sistema de items, conviertes MaterialData a ItemData correspondiente
            // outputInventory.AddItemFromMaterial(currentRecipe.outputMaterial);
        }

        IsProcessing = false;
        progress = 0f;
        // Trigger eventos, sonido, animaci�n, etc.
    }
}
