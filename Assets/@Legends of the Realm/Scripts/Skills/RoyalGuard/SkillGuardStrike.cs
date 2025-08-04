using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/RoyalGuard/GuardStrike")]
public class SkillGuardStrike : SkillBase
{
    public float damagePercentPerHit = 1f;
    public int maxCombo = 3;
    public float timeBetweenHits = 0.3f;

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        if (!IsReady()) yield break;

        for (int i = 0; i < maxCombo; i++)
        {
            float baseDamage = user.Stats.CalculatePhysicalAttackDamage() * damagePercentPerHit;
            target.TakePhysicalDamage(baseDamage);
            Debug.Log($"{target.name} recibió {baseDamage:F1} de daño en golpe {i + 1}");
            yield return new WaitForSeconds(timeBetweenHits);
        }
    }
}
