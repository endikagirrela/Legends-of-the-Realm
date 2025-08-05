using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    public string effectName;
    [TextArea] public string description;
    public StatusEffectType effectType;
    public Sprite icon;
    public float duration = 5f;

    public abstract void ApplyEffect(CharacterBase target, StatusEffectInstance instance);
    public abstract void RemoveEffect(CharacterBase target, StatusEffectInstance instance);
}

