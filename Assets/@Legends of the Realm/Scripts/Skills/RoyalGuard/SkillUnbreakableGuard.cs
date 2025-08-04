using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/RoyalGuard/UnbreakableGuard")]
public class SkillUnbreakableGuard : SkillBase
{
    public float defenseBoostPercent = 0.4f;
    public float attackSpeedPenaltyPercent = 0.2f;
    public float duration = 8f;

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        if (!IsReady()) yield break;
        yield return user.StartCoroutine(ApplyBuff(user));
    }

    private IEnumerator ApplyBuff(CharacterBase user)
    {
        float originalPhysicalRes = user.Stats.physicalResistance;
        float originalMagicalRes = user.Stats.magicalResistance;
        float originalAttackSpeed = user.Stats.attackSpeed;

        user.Stats.physicalResistance += originalPhysicalRes * defenseBoostPercent;
        user.Stats.magicalResistance += originalMagicalRes * defenseBoostPercent;
        user.Stats.attackSpeed -= originalAttackSpeed * attackSpeedPenaltyPercent;

        yield return new WaitForSeconds(duration);

        user.Stats.physicalResistance = originalPhysicalRes;
        user.Stats.magicalResistance = originalMagicalRes;
        user.Stats.attackSpeed = originalAttackSpeed;

        TriggerCooldown();
    }
}
