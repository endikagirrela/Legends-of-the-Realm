using UnityEngine;

[CreateAssetMenu(menuName = "Status Effects/Freeze")]
public class FreezeEffect : StatusEffect
{
    public float slowAmount = 0.5f;

    public override void ApplyEffect(CharacterBase target, StatusEffectInstance instance)
    {
        target.isFrozen = true;
        target.stats.movementSpeed.flatBonusBuff -= slowAmount;
    }

    public override void RemoveEffect(CharacterBase target, StatusEffectInstance instance)
    {
        target.isFrozen = false;
        target.stats.movementSpeed.flatBonusBuff += slowAmount;
    }
}
