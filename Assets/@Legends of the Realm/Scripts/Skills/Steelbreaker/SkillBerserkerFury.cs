using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Steelbreaker/BerserkerFury")]
public class SkillBerserkerFury : SkillBase
{
    public float damageBoostPercent = 0.25f;
    public float attackSpeedBoostPercent = 0.2f;
    public float hpCostPercent = 0.1f; // 10% max HP
    public float duration = 8f;
    public float cooldownOverride = 25f;

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        if (!IsReady()) yield break;
        yield return user.StartCoroutine(ApplyFury(user));
    }

    private IEnumerator ApplyFury(CharacterBase user)
    {
        float originalPhysDamage = user.Stats.physicalDamage;
        float originalAttackSpeed = user.Stats.attackSpeed;

        // Consume vida
        float hpCost = user.Stats.maxHP * hpCostPercent;
        user.Stats.TakePhysicalDamage(hpCost); // se puede usar un método de daño directo

        user.Stats.physicalDamage += originalPhysDamage * damageBoostPercent;
        user.Stats.attackSpeed += originalAttackSpeed * attackSpeedBoostPercent;

        yield return new WaitForSeconds(duration);

        user.Stats.physicalDamage = originalPhysDamage;
        user.Stats.attackSpeed = originalAttackSpeed;

        TriggerCooldown();
    }
}
