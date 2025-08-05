using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Status Effects/Burn")]
public class BurnEffect : StatusEffect
{
    public float tickInterval = 1f;

    public override void ApplyEffect(CharacterBase target, StatusEffectInstance instance)
    {
        target.StartCoroutine(DamageOverTime(target, instance));
    }

    private IEnumerator DamageOverTime(CharacterBase target, StatusEffectInstance instance)
    {
        while (!instance.IsExpired)
        {
            target.TakeMagicalDamage(instance.magnitude);
            yield return new WaitForSeconds(tickInterval);
        }
    }

    public override void RemoveEffect(CharacterBase target, StatusEffectInstance instance)
    {
        // No cleanup needed
    }
}
