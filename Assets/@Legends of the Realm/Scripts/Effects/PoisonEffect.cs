using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Status Effects/Poison")]
public class PoisonEffect : StatusEffect
{
    public float tickInterval = 1f;

    public override void ApplyEffect(CharacterBase target, StatusEffectInstance instance)
    {
        target.StartCoroutine(PoisonTick(target, instance));
    }

    private IEnumerator PoisonTick(CharacterBase target, StatusEffectInstance instance)
    {
        while (!instance.IsExpired)
        {
            target.TakePhysicalDamage(instance.magnitude);
            yield return new WaitForSeconds(tickInterval);
        }
    }

    public override void RemoveEffect(CharacterBase target, StatusEffectInstance instance)
    {
        // No cleanup needed
    }
}
