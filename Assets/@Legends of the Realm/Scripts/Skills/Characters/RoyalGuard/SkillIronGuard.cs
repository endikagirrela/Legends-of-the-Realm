using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/RoyalGuard/IronGuard")]
public class SkillIronGuard : SkillBase
{
    public StatusEffect buff_PhysicalDefense;
    public StatusEffect buff_MagicalDefense;
    public float buffMagnitude = 15f;
    public float buffDuration = 8f;

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        var manager = user.GetComponent<StatusEffectManager>();
        if (manager != null)
        {
            manager.ApplyEffect(buff_PhysicalDefense, buffMagnitude, buffDuration);
            manager.ApplyEffect(buff_MagicalDefense, buffMagnitude, buffDuration);
        }

        TriggerCooldown();
        yield return null;
    }
}
