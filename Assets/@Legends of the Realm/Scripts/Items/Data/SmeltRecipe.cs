using UnityEngine;

[CreateAssetMenu(menuName = "Materials/SmeltRecipe")]
public class SmeltRecipe : ScriptableObject
{
    public string recipeName;          // Ej: "Fundir Cobre"
    public OreData inputOre;           // Mena
    public MaterialData outputMaterial; // Mineral resultante
    public float smeltTimeSeconds;     // Tiempo de fundici�n (ej: 5f)
}
