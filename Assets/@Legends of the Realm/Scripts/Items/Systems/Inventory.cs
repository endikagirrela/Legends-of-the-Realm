using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Items")]
    public List<ItemData> items = new List<ItemData>();
    public int maxItemSlots = 30;

    [Header("Materials (stackable)")]
    // MaterialData es la clave; el valor es cuántas unidades tienes
    private Dictionary<MaterialData, int> materialStacks = new Dictionary<MaterialData, int>();

    // -------- ITEM METHODS --------
    public bool AddItem(ItemData item)
    {
        if (items.Count >= maxItemSlots) return false;
        items.Add(item);
        return true;
    }

    public void RemoveItem(ItemData item)
    {
        items.Remove(item);
    }

    // -------- MATERIAL METHODS --------
    public void AddMaterial(MaterialData material, int amount = 1)
    {
        if (material == null || amount <= 0) return;

        if (materialStacks.ContainsKey(material))
            materialStacks[material] += amount;
        else
            materialStacks[material] = amount;
    }

    public bool RemoveMaterial(MaterialData material, int amount = 1)
    {
        if (material == null || amount <= 0) return false;

        if (!materialStacks.TryGetValue(material, out int have) || have < amount)
            return false;

        materialStacks[material] = have - amount;
        if (materialStacks[material] <= 0)
            materialStacks.Remove(material);
        return true;
    }

    public int GetMaterialCount(MaterialData material)
    {
        if (material == null) return 0;
        return materialStacks.TryGetValue(material, out int count) ? count : 0;
    }

    // Optional: obtener todos los materiales para UI
    public IReadOnlyDictionary<MaterialData, int> GetAllMaterialStacks()
    {
        return materialStacks;
    }

    // Helper que convierte un MaterialData en un ItemData si tienes una receta o wrapping (depende de tu diseño)
    public bool AddItemFromMaterial(MaterialData material, ItemData wrapperItem)
    {
        // Ejemplo: podrías mapear material a un item específico y luego:
        return AddItem(wrapperItem);
    }
}
