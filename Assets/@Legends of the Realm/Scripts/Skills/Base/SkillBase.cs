using UnityEngine;
using System.Collections;

public abstract class SkillBase : ScriptableObject
{
    public string skillName;
    [TextArea] public string description;
    public Sprite icon;

    [Header("Mechanics")]
    public float cooldown = 5f;
    [HideInInspector] public float lastUsedTime = -999f;

    public bool IsReady() => Time.time >= lastUsedTime + cooldown;
    public void TriggerCooldown() => lastUsedTime = Time.time;

    // Método que se implementa en cada habilidad personalizada
    public abstract IEnumerator Execute(CharacterBase user, CharacterBase target);
    public void ResetCooldown()
    {
        lastUsedTime = -999f; // o -999f para asegurar que esté listo
    }
}
