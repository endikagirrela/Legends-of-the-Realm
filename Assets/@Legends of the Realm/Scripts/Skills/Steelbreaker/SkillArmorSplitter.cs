using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/Steelbreaker/ArmorSplitter")]
public class SkillArmorSplitter : SkillBase
{
    public float damagePercent = 1.2f; // 120%
    public float armorIgnorePercent = 0.4f; // ignora 40% resistencia física

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        if (!IsReady()) yield break;

        float raw = user.Stats.CalculatePhysicalAttackDamage() * damagePercent;

        // Simula ignorar parte de la resistencia física: temporalmente reduce resistencia del target para este hit
        float originalPhysRes = target.Stats.physicalResistance;
        target.Stats.physicalResistance *= (1f - armorIgnorePercent); // ignora 40%
        target.Stats.TakePhysicalDamage(raw);
        target.Stats.physicalResistance = originalPhysRes;

        RegisterRageHit(user);
        TriggerCooldown();
    }

    private void RegisterRageHit(CharacterBase user)
    {
        foreach (var field in user.GetType().GetField("activeEffects", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(user) as System.Collections.IList)
        {
            if (field is UnstoppableRageEffect rage)
            {
                rage.RegisterHit();
                break;
            }
        }
    }
}
