using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/RoyalGuard/UnstoppableWall")]
public class SkillUnstoppableWall : SkillBase
{
    public float damageReductionPercent = 0.7f;
    public float duration = 6f;
    public float wallWidth = 3f;

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        if (!IsReady()) yield break;
        yield return user.StartCoroutine(SpawnWall(user));
    }

    private IEnumerator SpawnWall(CharacterBase user)
    {
        List<CharacterBase> allies = AuraFetcher.GetAlliesBehind(user, wallWidth);
        foreach (var ally in allies)
        {
            ally.ApplyStatusEffect(new DamageReductionEffect(damageReductionPercent, duration, true));
        }
        yield return new WaitForSeconds(duration);
        TriggerCooldown();
    }
}
