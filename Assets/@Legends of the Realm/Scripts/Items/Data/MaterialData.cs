using UnityEngine;

[CreateAssetMenu(menuName = "Materials/Material")]
public class MaterialData : ScriptableObject
{
    public string materialName;    // Ej: "Cobre"
    public Sprite icon;
    [TextArea] public string description;
    // Puedes agregar propiedades especiales: peso, calidad, bonus al forjar, etc.
}
