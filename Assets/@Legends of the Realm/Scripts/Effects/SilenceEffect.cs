using UnityEngine;

[CreateAssetMenu(menuName = "Status Effects/Silence")]
public class SilenceEffect : StatusEffect
{
    public override void ApplyEffect(CharacterBase target, StatusEffectInstance instance)
    {
        target.isSilenced = true;
    }

    public override void RemoveEffect(CharacterBase target, StatusEffectInstance instance)
    {
        target.isSilenced = false;
    }
}
