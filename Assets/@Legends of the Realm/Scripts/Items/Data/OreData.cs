using UnityEngine;
using static TreeEditor.TreeEditorHelper;

[CreateAssetMenu(menuName = "Materials/Ore")]
public class OreData : ScriptableObject
{
    public string oreName;             // Ej: "Mena de Cobre"
    public Sprite icon;                // Para UI si quieres mostrarla
    public OreType oreType;            // Enum opcional si quieres categorizar
    public SmeltRecipe smeltRecipe;    // A qué mineral se funde
}
