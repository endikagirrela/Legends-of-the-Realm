using UnityEngine;

[CreateAssetMenu(menuName = "Status Effects/Stun")]
public class StunEffect : StatusEffect
{
    public override void ApplyEffect(CharacterBase target, StatusEffectInstance instance)
    {
        target.isStunned = true;
        target.InterruptAutoAttack();
    }

    public override void RemoveEffect(CharacterBase target, StatusEffectInstance instance)
    {
        target.isStunned = false;
    }
}
