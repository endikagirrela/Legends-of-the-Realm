using UnityEngine;

public abstract class PassiveSkillBase : ScriptableObject
{
    public string passiveName;
    [TextArea] public string description;

    public abstract void ApplyPassiveEffect(CharacterBase character);
}
