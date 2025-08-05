using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/RoyalGuard/TwinSlash")]
public class SkillTwinSlash : SkillBase
{
    public float hit1Damage = 15f;
    public float hit2Damage = 20f;
    public float timeBetweenHits = 0.3f;

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        if (target == null || target.stats.IsDead || !user.IsInRange(target)) yield break;

        target.TakePhysicalDamage(hit1Damage);
        yield return new WaitForSeconds(timeBetweenHits);
        target.TakePhysicalDamage(hit2Damage);

        TriggerCooldown();
    }
}
