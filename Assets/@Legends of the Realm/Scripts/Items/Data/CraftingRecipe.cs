using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public string recipeName;                      // Nombre (“Espada de Cobre”, “Anillo de Lunargenta”)
    public ItemData resultItem;                    // Item final (arma, armadura, anillo...)
    public List<MaterialRequirement> materials;    // Materiales necesarios
    public CraftingStationType stationRequired;    // Qué estación se necesita (forja, taller, alquimia, etc.)
    public float craftTime = 2f;                  // Tiempo de crafteo
    public float successChance = 1f;              // 0..1 (puede fallar si <1)
    public bool consumesMaterialsOnFail = true;    // si fallas, ¿se gastan?
}

[System.Serializable]
public class MaterialRequirement
{
    public MaterialData material; // lo que pide (puede ser un mineral, gema, etc.)
    public int amount = 1;        // cantidad requerida
}

public enum CraftingStationType
{
    Forge,
    Workshop,
    Alchemy,
    Jeweler,
    // etc.
}
