using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/RoyalGuard/AutoAttack")]
public class RoyalGuardAutoAttack : SkillAutoAttack
{
    public float baseDamage = 10f;
    public float interval = 1f;

    public override IEnumerator AutoAttackLoop(CharacterBase user, CharacterBase target)
    {
        Debug.Log("[AutoAttack] Loop iniciado");
        while (target != null && !target.stats.IsDead)
        {
            if (user.isStunned)
            {
                yield return new WaitForSeconds(0.1f);
                continue;
            }

            if (user.IsInRange(target))
            {
                float delay = interval / user.stats.attackSpeed.TotalValue;
                float damage = user.stats.physicalDamage.TotalValue + baseDamage;

                Debug.Log($"[AutoAttack] Golpe: {damage}");
                target.TakePhysicalDamage(damage);
                yield return new WaitForSeconds(delay);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }

        Debug.Log("[AutoAttack] Loop terminado");
    }
    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        Debug.Log("[AutoAttack] Ejecutado vía SkillBase");

        if (user.autoAttack != null)
        {
            user.autoAttack.StartAutoAttack(target);
        }

        yield return null;
    }

}
